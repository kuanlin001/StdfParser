using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StdfLib
{
    public class StdfReader
    {
        public enum Endian
        {
            Unknown, Big, Little,
        }

        public delegate void recordProcessedDelegate(string recName, string recField, string fieldDataType, object recData);
        public delegate bool abortParseDelegate();
        public delegate void headerProcessedDelegate(string recName, int recType, int recSubType, int recLength);

        public recordProcessedDelegate onRecordProcessed = null;
        public abortParseDelegate abordProcess = null;
        public headerProcessedDelegate onHeaderProcessed = null;

        string inputFile = "";
        List<string> recOfInterest;

        public string InputFile
        {
            get { return inputFile; }
            set
            {
                inputFile = value.Trim();
                if (inputFile == "" || !File.Exists(inputFile))
                    throw new Exception("input file not found");
            }
        }

        public void ClearSettings()
        {
            recOfInterest = null;
            onRecordProcessed = null;
            abordProcess = null;
            onHeaderProcessed = null;
        }

        public void SetRecOfInterest(string[] recs)
        {
            if (recs == null)
                recOfInterest = null;
            else
            {
                if (recOfInterest == null)
                    recOfInterest = new List<string>();
                else
                    recOfInterest.Clear();
                recOfInterest.AddRange(recs);
            }
        }

        public StdfReader(string filePath)
        {
            inputFile = filePath.Trim();
            if (inputFile == "" || !File.Exists(inputFile))
                throw new Exception("input file not found");
        }

        public void parseFile(bool header_only = false)
        {
            using (FileStream fs = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BinaryReader reader = null;
                int cpu_type = -1;
                int stdf_ver = -1;
                Endian endi = Endian.Unknown;

                if (inputFile.ToLower().EndsWith(".gz"))
                    reader = new BinaryReader(new System.IO.Compression.GZipStream(fs, System.IO.Compression.CompressionMode.Decompress));
                else
                    reader = new BinaryReader(fs);

                try
                {
                    getFAR(reader, out cpu_type, out stdf_ver);

                    if (stdf_ver != 3 && stdf_ver != 4)
                        throw new Exception("unknown cpu type");

                    if (cpu_type <= 0)
                        throw new Exception("unknown cpu type");
                    else
                    {
                        if (cpu_type == 1)
                            endi = Endian.Big;
                        else if (cpu_type == 2)
                            endi = Endian.Little;
                    }

                    int recLength = -1;
                    int recType = -1;
                    int recSubType = -1;

                    while (true)
                    {
                        if (abordProcess != null && abordProcess())
                            break;

                        if (getHeader(reader, endi, out recLength, out recType, out recSubType) <= 0)
                            break;

                        string recName = StdfSpec.lookupRecord(recType, recSubType, stdf_ver);
                        if (onHeaderProcessed != null)
                            onHeaderProcessed(recName, recType, recSubType, recLength);

                        //need to implement error message when recName == "" (unknown records)
                        if (!header_only && recName != "" && (recOfInterest == null || recOfInterest.Count == 0 || recOfInterest.Contains(recName)))
                        {
                            processRecord(reader, endi, recLength, recName, stdf_ver);
                        }
                        else
                        {
                            if (reader.BaseStream.CanSeek)
                                reader.BaseStream.Seek(recLength, SeekOrigin.Current);
                            else
                                reader.ReadBytes(recLength);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("unhandled exception: " + ex.Message);
                }

                reader.Close();
                reader.Dispose();
            }
        }

        private void processRecord(BinaryReader reader, Endian endi, int recLength, string recName, int stdf_ver)
        {
            var buf = ReadBytes(reader, recLength, endi);
            int buf_pos = 0;
            foreach (Tuple<string, string> f in StdfSpec.getRecFields(recName, stdf_ver))
            {
                string fname = f.Item1;
                string dataType = f.Item2;
                object dataVal = readRecData(buf, dataType.ToUpper(), ref buf_pos);

                if (onRecordProcessed != null)
                    onRecordProcessed(recName, fname, dataType, dataVal);
            }
        }

        private object readRecData(byte[] buffer, string dataType, ref int buf_pos)
        {
            if (buf_pos >= buffer.Length)
                return null;

            object recData = null;

            if (dataType == "N1")
            {
                recData = buffer[buf_pos];
                buf_pos += 1;
            }
            else if (dataType == "U4")
            {
                recData = BitConverter.ToUInt32(buffer, buf_pos);
                buf_pos += 4;
            }
            else if (dataType == "I4")
            {
                recData = BitConverter.ToInt32(buffer, buf_pos);
                buf_pos += 4;
            }
            else if (dataType == "U2")
            {
                recData = BitConverter.ToUInt16(buffer, buf_pos);
                buf_pos += 2;
            }
            else if (dataType == "I2")
            {
                recData = BitConverter.ToInt16(buffer, buf_pos);
                buf_pos += 2;
            }
            else if (dataType == "U1" || dataType == "I1")
            {
                recData = Int32.Parse(buffer[buf_pos].ToString());
                buf_pos += 1;
            }
            else if (dataType == "R4")
            {
                recData = BitConverter.ToSingle(buffer, buf_pos);
                buf_pos += 4;
            }
            else if (dataType == "R8")
            {
                recData = BitConverter.ToDouble(buffer, buf_pos);
                buf_pos += 8;
            }
            else if (dataType == "C1")
            {
                recData = Encoding.ASCII.GetChars(buffer, buf_pos, 1)[0];
                buf_pos += 1;
            }
            else if (dataType == "CN")
            {
                int charCnt = Int32.Parse(buffer[buf_pos].ToString());
                buf_pos += 1;
                recData = Encoding.ASCII.GetString(buffer, buf_pos, charCnt);
                buf_pos += charCnt;
            }
            else if (dataType.StartsWith("C") && (dataType.Remove(0, 1)).Length > 0 && (dataType.Remove(0, 1)).All(c => Char.IsDigit(c)))
            {
                int charCnt = Int32.Parse((dataType.Remove(0, 1)));
                buf_pos += 1;
                recData = Encoding.ASCII.GetString(buffer, buf_pos, charCnt);
                buf_pos += charCnt;
            }
            else if (dataType == "B1")
            {
                recData = BitConverter.ToString(buffer, buf_pos, 1);
                buf_pos += 1;
            }
            else if (dataType == "BN")
            {
                int charCnt = Int32.Parse(buffer[buf_pos].ToString());
                buf_pos += 1;
                recData = BitConverter.ToString(buffer, buf_pos, charCnt);
                buf_pos += charCnt;
            }
            else if (dataType == "DN")
            {
                int charCnt = BitConverter.ToInt16(buffer, buf_pos);
                buf_pos += 2;
                recData = BitConverter.ToString(buffer, buf_pos, charCnt);
                buf_pos += charCnt;
            }
            else if (dataType == "VN")
            {
                var types = new string[] { "B0", "U1", "U2", "U4", "I1", "I2", "I4", "R4", "R8", "CN", "BN", "DN", "N1" };
                int type = Int32.Parse(buffer[buf_pos].ToString());
                buf_pos += 1;
                recData = readRecData(buffer, types[type], ref buf_pos);
            }
            else if (dataType.StartsWith("K"))
            {
                throw new Exception("Kx not implemented");
            }
            else
            {
                throw new Exception("unknown data type: " + dataType);
            }

            return recData;
        }

        private void getFAR(BinaryReader reader, out int cpu_type, out int stdf_ver)
        {
            var buf = reader.ReadBytes(6);
            if (buf.Length != 6)
                throw new Exception("error reading Far: unexpected buffer length.  file may be corrupt");
            cpu_type = Int32.Parse(buf[4].ToString());
            stdf_ver = Int32.Parse(buf[5].ToString());
        }

        private int getHeader(BinaryReader reader, Endian endi, out int recLength, out int recType, out int recSubType)
        {
            var buf = ReadBytes(reader, 4, endi);
            if (buf == null || buf.Length == 0)
            {
                //EOF reached
                recLength = 0; recType = -1; recSubType = -1;
            }
            else if (buf.Length != 4)
            {
                throw new Exception("error reading header: unexpected buffer length.  file may be corrupt.");
            }
            else
            {
                recLength = BitConverter.ToUInt16(buf, 0);
                recType = Int32.Parse(buf[2].ToString());
                recSubType = Int32.Parse(buf[3].ToString());
            }
            return buf.Length;
        }

        private void SwapBuffer(byte[] buffer, int length)
        {
            Array.Reverse(buffer, 0, length);
        }

        private byte[] ReadBytes(BinaryReader reader, int length, Endian endi)
        {
            var buf = reader.ReadBytes(length);
            if (buf != null && buf.Length > 0)
            {
                if (endi == Endian.Big)
                    SwapBuffer(buf, length);
            }

            return buf;
        }
    }
}

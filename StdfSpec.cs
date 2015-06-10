using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StdfLib
{
    static class StdfSpec
    {
        public static string lookupRecord(int recType, int recSubType, int version)
        {
            if (version != 3 && version != 4)
                throw new Exception("unsupported version: " + version.ToString());

            string key = "[" + recType.ToString() + "," + recSubType.ToString() + "]";

            if (version == 4)
            {
                if (V4.recMaps.ContainsKey(key))
                    return V4.recMaps[key];
                else
                    return "";
            }
            else
            {
                if (V3.recMaps.ContainsKey(key))
                    return V3.recMaps[key];
                else
                    return "";
            }

        }

        public static IEnumerable<Tuple<string, string>> getRecFields(string recName, int version)
        {
            if (version != 3 && version != 4)
                throw new Exception("unsupported version: " + version.ToString());

            if (version == 4)
            {
                if (V4.recFields.ContainsKey(recName))
                {
                    foreach (Tuple<string, string> rec in V4.recFields[recName])
                        yield return rec;
                }
                else
                    throw new Exception("unknown record name: " + recName);
            }
            else
            {
                if (V3.recFields.ContainsKey(recName))
                {
                    foreach (Tuple<string, string> rec in V3.recFields[recName])
                        yield return rec;
                }
                else
                    throw new Exception("unknown record name: " + recName);
            }
        }

        private static class V4
        {
            public static Dictionary<string, string> recMaps = new Dictionary<string, string>()
            {
                { "[0,10]",  "Far" },
                { "[0,20]",  "Atr" },
                { "[1,10]",  "Mir" },
                { "[1,20]",  "Mrr" },
                { "[1,30]",  "Pcr" },
                { "[1,40]",  "Hbr" },
                { "[1,50]",  "Sbr" },
                { "[1,60]",  "Pmr" },
                { "[1,62]",  "Pgr" },
                { "[1,63]",  "Plr" },
                { "[1,70]",  "Rdr" },
                { "[1,80]",  "Sdr" },
                { "[2,10]",  "Wir" },
                { "[2,20]",  "Wrr" },
                { "[2,30]",  "Wcr" },
                { "[5,10]",  "Pir" },
                { "[5,20]",  "Prr" },
                { "[10,30]",  "Tsr" },
                { "[15,10]",  "Ptr" },
                { "[15,15]",  "Mpr" },
                { "[15,20]",  "Ftr" },
                { "[20,10]",  "Bps" },
                { "[20,20]",  "Eps" },
                { "[50,10]",  "Gdr" },
                { "[50,30]",  "Dtr" },
            };

            public static Dictionary<string, List<Tuple<string, string>>> recFields = new Dictionary<string, List<Tuple<string, string>>>()
            {
                {
                    "Far", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("CPU_TYPE", "U1"),
                        Tuple.Create<string, string>("STDF_VER", "U1")
                    }
                },
                {
                    "Atr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("MOD_TIM", "U4"),
                        Tuple.Create<string, string>("CMD_LINE", "Cn")
                    }
                },
                {
                    "Mir", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("SETUP_T", "U4"),
                        Tuple.Create<string, string>("START_T", "U4"),
                        Tuple.Create<string, string>("STAT_NUM", "U1"),
                        Tuple.Create<string, string>("MODE_COD", "C1"),
                        Tuple.Create<string, string>("RTST_COD", "C1"),
                        Tuple.Create<string, string>("PROT_COD", "C1"),
                        Tuple.Create<string, string>("BURN_TIM", "U2"),
                        Tuple.Create<string, string>("CMOD_COD", "C1"),
                        Tuple.Create<string, string>("LOT_ID", "Cn"),
                        Tuple.Create<string, string>("PART_TYP", "Cn"),
                        Tuple.Create<string, string>("NODE_NAM", "Cn"),
                        Tuple.Create<string, string>("TSTR_TYP", "Cn"),
                        Tuple.Create<string, string>("JOB_NAM", "Cn"),
                        Tuple.Create<string, string>("JOB_REV", "Cn"),
                        Tuple.Create<string, string>("SBLOT_ID", "Cn"),
                        Tuple.Create<string, string>("OPER_NAM", "Cn"),
                        Tuple.Create<string, string>("EXEC_TYP", "Cn"),
                        Tuple.Create<string, string>("EXEC_VER", "Cn"),
                        Tuple.Create<string, string>("TEST_COD", "Cn"),
                        Tuple.Create<string, string>("TST_TEMP", "Cn"),
                        Tuple.Create<string, string>("USER_TXT", "Cn"),
                        Tuple.Create<string, string>("AUX_FILE", "Cn"),
                        Tuple.Create<string, string>("PKG_TYP", "Cn"),
                        Tuple.Create<string, string>("FAMLY_ID", "Cn"),
                        Tuple.Create<string, string>("DATE_COD", "Cn"),
                        Tuple.Create<string, string>("FACIL_ID", "Cn"),
                        Tuple.Create<string, string>("FLOOR_ID", "Cn"),
                        Tuple.Create<string, string>("PROC_ID", "Cn"),
                        Tuple.Create<string, string>("OPER_FRQ", "Cn"),
                        Tuple.Create<string, string>("SPEC_NAM", "Cn"),
                        Tuple.Create<string, string>("SPEC_VER", "Cn"),
                        Tuple.Create<string, string>("FLOW_ID", "Cn"),
                        Tuple.Create<string, string>("SETUP_ID", "Cn"),
                        Tuple.Create<string, string>("DSGN_REV", "Cn"),
                        Tuple.Create<string, string>("ENG_ID", "Cn"),
                        Tuple.Create<string, string>("ROM_COD", "Cn"),
                        Tuple.Create<string, string>("SERL_NUM", "Cn"),
                        Tuple.Create<string, string>("SUPR_NAM", "Cn")
                    }
                },
                {
                    "Mrr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("FINISH_T", "U4"),
                        Tuple.Create<string, string>("DISP_COD", "C1"),
                        Tuple.Create<string, string>("USR_DESC", "Cn"),
                        Tuple.Create<string, string>("EXC_DESC", "Cn")
                    }
                },
                {
                    "Pcr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_NUM","U1"),
                        Tuple.Create<string, string>("PART_CNT","U4"),
                        Tuple.Create<string, string>("RTST_CNT","U4"),
                        Tuple.Create<string, string>("ABRT_CNT","U4"),
                        Tuple.Create<string, string>("GOOD_CNT","U4"),
                        Tuple.Create<string, string>("FUNC_CNT","U4")
                    }
                },
                {
                    "Hbr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_NUM","U1"),
                        Tuple.Create<string, string>("HBIN_NUM","U2"),
                        Tuple.Create<string, string>("HBIN_CNT","U4"),
                        Tuple.Create<string, string>("HBIN_PF","C1"),
                        Tuple.Create<string, string>("HBIN_NAM","Cn")
                    }
                },
                {
                    "Sbr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_NUM","U1"),
                        Tuple.Create<string, string>("SBIN_NUM","U2"),
                        Tuple.Create<string, string>("SBIN_CNT","U4"),
                        Tuple.Create<string, string>("SBIN_PF","C1"),
                        Tuple.Create<string, string>("SBIN_NAM","Cn")
                    }
                },
                {
                    "Pmr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("PMR_INDX","U2"),
                        Tuple.Create<string, string>("CHAN_TYP","U2"),
                        Tuple.Create<string, string>("CHAN_NAM","Cn"),
                        Tuple.Create<string, string>("PHY_NAM","Cn"),
                        Tuple.Create<string, string>("LOG_NAM","Cn"),
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_NUM","U1")
                    }
                },
                {
                    "Pgr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("GRP_INDX","U2"),
                        Tuple.Create<string, string>("GRP_NAM","Cn"),
                        Tuple.Create<string, string>("INDX_CNT","U2"),
                        Tuple.Create<string, string>("PMR_INDX","K2U2")
                    }
                },
                {
                    "Plr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("GRP_CNT","U2"),
                        Tuple.Create<string, string>("GRP_INDX","K0U2"),
                        Tuple.Create<string, string>("GRP_MODE","K0U2"),
                        Tuple.Create<string, string>("GRP_RADX","K0U1"),
                        Tuple.Create<string, string>("PGM_CHAR","K0Cn"),
                        Tuple.Create<string, string>("RTN_CHAR","K0Cn"),
                        Tuple.Create<string, string>("PGM_CHAL","K0Cn"),
                        Tuple.Create<string, string>("RTN_CHAL","K0Cn")
                    }
                },
                {
                    "Rdr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("NUM_BINS","U2"),
                        Tuple.Create<string, string>("RTST_BIN","K0U2")
                    }
                },
                {
                    "Sdr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_GRP","U1"),
                        Tuple.Create<string, string>("SITE_CNT","U1"),
                        Tuple.Create<string, string>("SITE_NUM","K2U1"),
                        Tuple.Create<string, string>("HAND_TYP","Cn"),
                        Tuple.Create<string, string>("HAND_ID","Cn"),
                        Tuple.Create<string, string>("CARD_TYP","Cn"),
                        Tuple.Create<string, string>("CARD_ID","Cn"),
                        Tuple.Create<string, string>("LOAD_TYP","Cn"),
                        Tuple.Create<string, string>("LOAD_ID","Cn"),
                        Tuple.Create<string, string>("DIB_TYP","Cn"),
                        Tuple.Create<string, string>("DIB_ID","Cn"),
                        Tuple.Create<string, string>("CABL_TYP","Cn"),
                        Tuple.Create<string, string>("CABL_ID","Cn"),
                        Tuple.Create<string, string>("CONT_TYP","Cn"),
                        Tuple.Create<string, string>("CONT_ID","Cn"),
                        Tuple.Create<string, string>("LASR_TYP","Cn"),
                        Tuple.Create<string, string>("LASR_ID","Cn"),
                        Tuple.Create<string, string>("EXTR_TYP","Cn"),
                        Tuple.Create<string, string>("EXTR_ID","Cn")
                    }
                },
                {
                    "Wir", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_GRP","U1"),
                        Tuple.Create<string, string>("START_T","U4"),
                        Tuple.Create<string, string>("WAFER_ID","Cn")
                    }
                },
                {
                    "Wrr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_GRP","U1"),
                        Tuple.Create<string, string>("FINISH_T","U4"),
                        Tuple.Create<string, string>("PART_CNT","U4"),
                        Tuple.Create<string, string>("RTST_CNT","U4"),
                        Tuple.Create<string, string>("ABRT_CNT","U4"),
                        Tuple.Create<string, string>("GOOD_CNT","U4"),
                        Tuple.Create<string, string>("FUNC_CNT","U4"),
                        Tuple.Create<string, string>("WAFER_ID","Cn"),
                        Tuple.Create<string, string>("FABWF_ID","Cn"),
                        Tuple.Create<string, string>("FRAME_ID","Cn"),
                        Tuple.Create<string, string>("MASK_ID","Cn"),
                        Tuple.Create<string, string>("USR_DESC","Cn"),
                        Tuple.Create<string, string>("EXC_DESC","Cn")
                    }
                },
                {
                    "Wcr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("WAFR_SIZ","R4"),
                        Tuple.Create<string, string>("DIE_HT","R4"),
                        Tuple.Create<string, string>("DIE_WID","R4"),
                        Tuple.Create<string, string>("WF_UNITS","U1"),
                        Tuple.Create<string, string>("WF_FLAT","C1"),
                        Tuple.Create<string, string>("CENTER_X","I2"),
                        Tuple.Create<string, string>("CENTER_Y","I2"),
                        Tuple.Create<string, string>("POS_X","C1"),
                        Tuple.Create<string, string>("POS_Y","C1")
                    }
                },
                {
                    "Pir", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_NUM","U1")
                    }
                },
                {
                    "Prr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_NUM","U1"),
                        Tuple.Create<string, string>("PART_FLG","B1"),
                        Tuple.Create<string, string>("NUM_TEST","U2"),
                        Tuple.Create<string, string>("HARD_BIN","U2"),
                        Tuple.Create<string, string>("SOFT_BIN","U2"),
                        Tuple.Create<string, string>("X_COORD","I2"),
                        Tuple.Create<string, string>("Y_COORD","I2"),
                        Tuple.Create<string, string>("TEST_T","U4"),
                        Tuple.Create<string, string>("PART_ID","Cn"),
                        Tuple.Create<string, string>("PART_TXT","Cn"),
                        Tuple.Create<string, string>("PART_FIX","Bn")
                    }
                },
                {
                    "Tsr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_NUM","U1"),
                        Tuple.Create<string, string>("TEST_TYP","C1"),
                        Tuple.Create<string, string>("TEST_NUM","U4"),
                        Tuple.Create<string, string>("EXEC_CNT","U4"),
                        Tuple.Create<string, string>("FAIL_CNT","U4"),
                        Tuple.Create<string, string>("ALRM_CNT","U4"),
                        Tuple.Create<string, string>("TEST_NAM","Cn"),
                        Tuple.Create<string, string>("SEQ_NAME","Cn"),
                        Tuple.Create<string, string>("TEST_LBL","Cn"),
                        Tuple.Create<string, string>("OPT_FLAG","B1"),
                        Tuple.Create<string, string>("TEST_TIM","R4"),
                        Tuple.Create<string, string>("TEST_MIN","R4"),
                        Tuple.Create<string, string>("TEST_MAX","R4"),
                        Tuple.Create<string, string>("TST_SUMS","R4"),
                        Tuple.Create<string, string>("TST_SQRS","R4")
                    }
                },
                {
                    "Ptr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEST_NUM","U4"),
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_NUM","U1"),
                        Tuple.Create<string, string>("TEST_FLG","B1"),
                        Tuple.Create<string, string>("PARM_FLG","B1"),
                        Tuple.Create<string, string>("RESULT","R4"),
                        Tuple.Create<string, string>("TEST_TXT","Cn"),
                        Tuple.Create<string, string>("ALARM_ID","Cn"),
                        Tuple.Create<string, string>("OPT_FLAG","B1"),
                        Tuple.Create<string, string>("RES_SCAL","I1"),
                        Tuple.Create<string, string>("LLM_SCAL","I1"),
                        Tuple.Create<string, string>("HLM_SCAL","I1"),
                        Tuple.Create<string, string>("LO_LIMIT","R4"),
                        Tuple.Create<string, string>("HI_LIMIT","R4"),
                        Tuple.Create<string, string>("UNITS","Cn"),
                        Tuple.Create<string, string>("C_RESFMT","Cn"),
                        Tuple.Create<string, string>("C_LLMFMT","Cn"),
                        Tuple.Create<string, string>("C_HLMFMT","Cn"),
                        Tuple.Create<string, string>("LO_SPEC","R4"),
                        Tuple.Create<string, string>("HI_SPEC","R4")
                    }
                },
                {
                    "Mpr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEST_NUM","U4"),
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_NUM","U1"),
                        Tuple.Create<string, string>("TEST_FLG","B1"),
                        Tuple.Create<string, string>("PARM_FLG","B1"),
                        Tuple.Create<string, string>("RTN_ICNT","U2"),
                        Tuple.Create<string, string>("RSLT_CNT","U2"),
                        Tuple.Create<string, string>("RTN_STAT","K5N1"),
                        Tuple.Create<string, string>("RTN_RSLT","K6R4"),
                        Tuple.Create<string, string>("TEST_TXT","Cn"),
                        Tuple.Create<string, string>("ALARM_ID","Cn"),
                        Tuple.Create<string, string>("OPT_FLAG","B1"),
                        Tuple.Create<string, string>("RES_SCAL","I1"),
                        Tuple.Create<string, string>("LLM_SCAL","I1"),
                        Tuple.Create<string, string>("HLM_SCAL","I1"),
                        Tuple.Create<string, string>("LO_LIMIT","R4"),
                        Tuple.Create<string, string>("HI_LIMIT","R4"),
                        Tuple.Create<string, string>("START_IN","R4"),
                        Tuple.Create<string, string>("INCR_IN","R4"),
                        Tuple.Create<string, string>("RTN_INDX","K5U2"),
                        Tuple.Create<string, string>("UNITS","Cn"),
                        Tuple.Create<string, string>("UNITS_IN","Cn"),
                        Tuple.Create<string, string>("C_RESFMT","Cn"),
                        Tuple.Create<string, string>("C_LLMFMT","Cn"),
                        Tuple.Create<string, string>("C_HLMFMT","Cn"),
                        Tuple.Create<string, string>("LO_SPEC","R4"),
                        Tuple.Create<string, string>("HI_SPEC","R4")
                    }
                },
                {
                    "Ftr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEST_NUM","U4"),
                        Tuple.Create<string, string>("HEAD_NUM","U1"),
                        Tuple.Create<string, string>("SITE_NUM","U1"),
                        Tuple.Create<string, string>("TEST_FLG","B1"),
                        Tuple.Create<string, string>("OPT_FLAG","B1"),
                        Tuple.Create<string, string>("CYCL_CNT","U4"),
                        Tuple.Create<string, string>("REL_VADR","U4"),
                        Tuple.Create<string, string>("REPT_CNT","U4"),
                        Tuple.Create<string, string>("NUM_FAIL","U4"),
                        Tuple.Create<string, string>("XFAIL_AD","I4"),
                        Tuple.Create<string, string>("YFAIL_AD","I4"),
                        Tuple.Create<string, string>("VECT_OFF","I2"),
                        Tuple.Create<string, string>("RTN_ICNT","U2"),
                        Tuple.Create<string, string>("PGM_ICNT","U2"),
                        Tuple.Create<string, string>("RTN_INDX","K12U2"),
                        Tuple.Create<string, string>("RTN_STAT","K12N1"),
                        Tuple.Create<string, string>("PGM_INDX","K13U2"),
                        Tuple.Create<string, string>("PGM_STAT","K13N1"),
                        Tuple.Create<string, string>("FAIL_PIN","Dn"),
                        Tuple.Create<string, string>("VECT_NAM","Cn"),
                        Tuple.Create<string, string>("TIME_SET","Cn"),
                        Tuple.Create<string, string>("OP_CODE","Cn"),
                        Tuple.Create<string, string>("TEST_TXT","Cn"),
                        Tuple.Create<string, string>("ALARM_ID","Cn"),
                        Tuple.Create<string, string>("PROG_TXT","Cn"),
                        Tuple.Create<string, string>("RSLT_TXT","Cn"),
                        Tuple.Create<string, string>("PATG_NUM","U1"),
                        Tuple.Create<string, string>("SPIN_MAP","Dn")
                    }
                },
                {
                    "Bps", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("SEQ_NAME","Cn")
                    }
                },
                {
                    "Eps", new List<Tuple<string, string>>{ }
                },
                {
                    "Gdr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("GEN_DATA","Vn")
                    }
                },
                {
                    "Dtr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEXT_DAT","Cn")
                    }
                }
            };
        }

        private static class V3
        {
            public static Dictionary<string, string> recMaps = new Dictionary<string, string>()
            {
                { "[0,10]",  "Far" },
                { "[1,10]",  "Mir" },
                { "[1,20]",  "Mrr" },
                { "[2,10]",  "Wir" },
                { "[2,20]",  "Wrr" },
                { "[1,40]",  "Hbr" },
                { "[1,50]",  "Sbr" },
                { "[1,30]",  "Tsr" },
                { "[5,10]",  "Pir" },
                { "[5,20]",  "Prr" },
                { "[10,20]",  "Fdr" },
                { "[15,20]",  "Ftr" },
                { "[10,10]",  "Pdr" },
                { "[15,10]",  "Ptr" },
                { "[220,201]",  "Brr" },
                { "[220,202]",  "Wtr" },
                { "[220,203]",  "Etsr" },
                { "[220,203]",  "EtsrV3" },
                { "[220,204]",  "Gtr" },
                { "[220,205]",  "Adr" },
                { "[220,206]",  "Epdr" },
                { "[50,10]",  "Gdr" },
                { "[25,10]",  "Shb" },
                { "[25,20]",  "Ssb" },
                { "[25,30]",  "Sts" },
                { "[25,40]",  "Scr" },
                { "[20,10]",  "Bps" },
                { "[20,20]",  "Eps" },
                { "[50,30]",  "Dtr" }
            };

            public static Dictionary<string, List<Tuple<string, string>>> recFields = new Dictionary<string, List<Tuple<string, string>>>()
            {
                {
                    "Far", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("CPU_TYPE", "U1"),
                        Tuple.Create<string, string>("STDF_VER", "U1")
                    }
                },
                {
                    "Mir", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("CPU_TYPE", "U1"),
                        Tuple.Create<string, string>("STDF_VER", "U1"),
                        Tuple.Create<string, string>("MODE_COD", "C1"),
                        Tuple.Create<string, string>("STAT_NUM", "U1"),
                        Tuple.Create<string, string>("TEST_COD", "C3"),
                        Tuple.Create<string, string>("RTST_COD", "C1"),
                        Tuple.Create<string, string>("PROT_COD", "C1"),
                        Tuple.Create<string, string>("CMOD_COD", "C1"),
                        Tuple.Create<string, string>("SETUP_T", "U4"),
                        Tuple.Create<string, string>("START_T", "U4"),
                        Tuple.Create<string, string>("LOT_ID", "Cn"),
                        Tuple.Create<string, string>("PART_TYP", "Cn"),
                        Tuple.Create<string, string>("JOB_NAM", "Cn"),
                        Tuple.Create<string, string>("OPER_NAM", "Cn"),
                        Tuple.Create<string, string>("NODE_NAM", "Cn"),
                        Tuple.Create<string, string>("TSTR_TYP", "Cn"),
                        Tuple.Create<string, string>("EXEC_TYP", "Cn"),
                        Tuple.Create<string, string>("SUPR_NAM", "Cn"),
                        Tuple.Create<string, string>("HAND_ID", "Cn"),
                        Tuple.Create<string, string>("SBLOT_ID", "Cn"),
                        Tuple.Create<string, string>("JOB_REV", "Cn"),
                        Tuple.Create<string, string>("PROC_ID", "Cn"),
                        Tuple.Create<string, string>("PRB_CARD", "Cn")
                    }
                },
                {
                    "Mrr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("FINISH_T", "U4"),
                        Tuple.Create<string, string>("PART_CNT", "U4"),
                        Tuple.Create<string, string>("RTST_CNT", "I4"),
                        Tuple.Create<string, string>("ABRT_CNT", "I4"),
                        Tuple.Create<string, string>("GOOD_CNT", "I4"),
                        Tuple.Create<string, string>("FUNC_CNT", "I4"),
                        Tuple.Create<string, string>("DISP_COD", "C1"),
                        Tuple.Create<string, string>("USR_DESC", "Cn"),
                        Tuple.Create<string, string>("EXC_DESC", "Cn")
                    }
                },
                {
                    "Wir", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HEAD_NUM", "U1"),
                        Tuple.Create<string, string>("PAD_BYTE", "B1"),
                        Tuple.Create<string, string>("START_T", "U4"),
                        Tuple.Create<string, string>("WAFER_ID", "Cn")
                    }
                },
                {
                    "Wrr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("FINISH_T", "U4"),
                        Tuple.Create<string, string>("HEAD_NUM", "U1"),
                        Tuple.Create<string, string>("PAD_BYTE", "B1"),
                        Tuple.Create<string, string>("PART_CNT", "U4"),
                        Tuple.Create<string, string>("RTST_CNT", "I4"),
                        Tuple.Create<string, string>("ABRT_CNT", "I4"),
                        Tuple.Create<string, string>("GOOD_CNT", "I4"),
                        Tuple.Create<string, string>("FUNC_CNT", "I4"),
                        Tuple.Create<string, string>("WAFER_ID", "Cn"),
                        Tuple.Create<string, string>("HAND_ID", "Cn"),
                        Tuple.Create<string, string>("PRB_CARD", "Cn"),
                        Tuple.Create<string, string>("USR_DESC", "Cn"),
                        Tuple.Create<string, string>("EXC_DESC", "Cn")
                    }
                },
                {
                    "Hbr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HBIN_NUM", "U2"),
                        Tuple.Create<string, string>("HBIN_CNT", "U4"),
                        Tuple.Create<string, string>("HBIN_NAM", "Cn")
                    }
                },
                {
                    "Sbr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("SBIN_NUM", "U2"),
                        Tuple.Create<string, string>("SBIN_CNT", "U4"),
                        Tuple.Create<string, string>("SBIN_NAM", "Cn")
                    }
                },
                {
                    "Tsr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEST_NUM", "U4"),
                        Tuple.Create<string, string>("EXEC_CNT", "I4"),
                        Tuple.Create<string, string>("FAIL_CNT", "I4"),
                        Tuple.Create<string, string>("ALRM_CNT", "I4"),
                        Tuple.Create<string, string>("OPT_FLAG", "B1"),
                        Tuple.Create<string, string>("PAD_BYTE", "B1"),
                        Tuple.Create<string, string>("TEST_MIN", "R4"),
                        Tuple.Create<string, string>("TEST_MAX", "R4"),
                        Tuple.Create<string, string>("TST_MEAN", "R4"),
                        Tuple.Create<string, string>("TST_SDEV", "R4"),
                        Tuple.Create<string, string>("TST_SUMS", "R4"),
                        Tuple.Create<string, string>("TST_SQRS", "R4"),
                        Tuple.Create<string, string>("TEST_NAM", "Cn"),
                        Tuple.Create<string, string>("SEQ_NAME", "Cn")
                    }
                },
                {
                    "Pir", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HEAD_NUM", "U1"),
                        Tuple.Create<string, string>("SITE_NUM", "U1"),
                        Tuple.Create<string, string>("X_COORD", "I2"),
                        Tuple.Create<string, string>("Y_COORD", "I2"),
                        Tuple.Create<string, string>("PART_ID", "Cn")
                    }
                },
                {
                    "Prr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("HEAD_NUM", "U1"),
                        Tuple.Create<string, string>("SITE_NUM", "U1"),
                        Tuple.Create<string, string>("NUM_TEST", "U2"),
                        Tuple.Create<string, string>("HARD_BIN", "U2"),
                        Tuple.Create<string, string>("SOFT_BIN", "U2"),
                        Tuple.Create<string, string>("PART_FLG", "B1"),
                        Tuple.Create<string, string>("PAD_BYTE", "B1"),
                        Tuple.Create<string, string>("X_COORD", "I2"),
                        Tuple.Create<string, string>("Y_COORD", "I2"),
                        Tuple.Create<string, string>("PART_ID", "Cn"),
                        Tuple.Create<string, string>("PART_TXT", "Cn"),
                        Tuple.Create<string, string>("PART_FIX", "Bn")
                    }
                },
                {
                    "Fdr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEST_NUM", "U4"),
                        Tuple.Create<string, string>("DESC_FLG", "B1"),
                        Tuple.Create<string, string>("TEST_NAM", "Cn"),
                        Tuple.Create<string, string>("SEQ_NAME", "Cn")
                    }
                },
                {
                    "Ftr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEST_NUM", "U4"),
                        Tuple.Create<string, string>("HEAD_NUM", "U1"),
                        Tuple.Create<string, string>("SITE_NUM", "U1"),
                        Tuple.Create<string, string>("TEST_FLG", "B1"),
                        Tuple.Create<string, string>("DESC_FLG", "B1"),
                        Tuple.Create<string, string>("OPT_FLAG", "B1"),
                        Tuple.Create<string, string>("TIME_SET", "U1"),
                        Tuple.Create<string, string>("VECT_ADR", "U4"),
                        Tuple.Create<string, string>("CYCL_CNT", "U4"),
                        Tuple.Create<string, string>("REPT_CNT", "U2"),
                        Tuple.Create<string, string>("PCP_ADR", "U2"),
                        Tuple.Create<string, string>("NUM_FAIL", "U4"),
                        Tuple.Create<string, string>("FAIL_PIN", "Bn"),
                        Tuple.Create<string, string>("VECT_DAT", "Bn"),
                        Tuple.Create<string, string>("DEV_DAT", "Bn"),
                        Tuple.Create<string, string>("RPIN_MAP", "Bn"),
                        Tuple.Create<string, string>("TEST_NAM", "Cn"),
                        Tuple.Create<string, string>("SEQ_NAME", "Cn"),
                        Tuple.Create<string, string>("TEST_TXT", "Cn")
                    }
                },
                {
                    "Pdr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEST_NUM", "U4"),
                        Tuple.Create<string, string>("HEAD_NUM", "U1"),
                        Tuple.Create<string, string>("SITE_NUM", "U1"),
                        Tuple.Create<string, string>("TEST_FLG", "B1"),
                        Tuple.Create<string, string>("PARM_FLG", "B1"),
                        Tuple.Create<string, string>("RESULT", "R4"),
                        Tuple.Create<string, string>("OPT_FLAG", "B1"),
                        Tuple.Create<string, string>("RES_SCAL", "I1"),
                        Tuple.Create<string, string>("RES_LDIG", "U1"),
                        Tuple.Create<string, string>("RES_RDIG", "U1"),
                        Tuple.Create<string, string>("DESC_FLG", "B1"),
                        Tuple.Create<string, string>("UNITS", "C7"),
                        Tuple.Create<string, string>("LLM_SCAL", "I1"),
                        Tuple.Create<string, string>("HLM_SCAL", "I1"),
                        Tuple.Create<string, string>("LLM_LDIG", "U1"),
                        Tuple.Create<string, string>("LLM_RDIG", "U1"),
                        Tuple.Create<string, string>("HLM_LDIG", "U1"),
                        Tuple.Create<string, string>("HLM_RDIG", "U1"),
                        Tuple.Create<string, string>("LO_LIMIT", "R4"),
                        Tuple.Create<string, string>("HI_LIMIT", "R4"),
                        Tuple.Create<string, string>("TEST_NAM", "Cn"),
                        Tuple.Create<string, string>("SEQ_NAME", "Cn"),
                        Tuple.Create<string, string>("TEST_TXT", "Cn")
                    }
                },
                {
                    "Brr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("RTST_COD", "C1"),
                        Tuple.Create<string, string>("BIN_CNT", "U2"),
                        Tuple.Create<string, string>("BIN_NUM", "U2")
                    }
                },
                {
                    "Wtr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEST_TYPE", "C1")
                    }
                },
                {
                    "Etsr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEST_NUM", "U4"),
                        Tuple.Create<string, string>("EXEC_CNT", "I4"),
                        Tuple.Create<string, string>("FAIL_CNT", "I4"),
                        Tuple.Create<string, string>("ALRM_CNT", "I4"),
                        Tuple.Create<string, string>("TEST_10", "R4"),
                        Tuple.Create<string, string>("TEST_90", "R4"),
                        Tuple.Create<string, string>("OPT_FLAG", "B1"),
                        Tuple.Create<string, string>("PAD_BYTE", "B1"),
                        Tuple.Create<string, string>("TEST_MIN", "R4"),
                        Tuple.Create<string, string>("TEST_MAX", "R4"),
                        Tuple.Create<string, string>("TST_MEAN", "R4"),
                        Tuple.Create<string, string>("TST_SDEV", "R4"),
                        Tuple.Create<string, string>("TST_SUMS", "R4"),
                        Tuple.Create<string, string>("TST_SQRS", "R4"),
                        Tuple.Create<string, string>("TEST_NAM", "Cn"),
                        Tuple.Create<string, string>("SEQ_NAME", "Cn")
                    }
                },
                {
                    "EtsrV3", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEST_NUM", "U4"),
                        Tuple.Create<string, string>("EXEC_CNT", "I4"),
                        Tuple.Create<string, string>("FAIL_CNT", "I4"),
                        Tuple.Create<string, string>("ALRM_CNT", "I4"),
                        Tuple.Create<string, string>("OPT_FLAG_QU", "B1"),
                        Tuple.Create<string, string>("TEST_05", "R4"),
                        Tuple.Create<string, string>("TEST_10", "R4"),
                        Tuple.Create<string, string>("TEST_50", "R4"),
                        Tuple.Create<string, string>("TEST_90", "R4"),
                        Tuple.Create<string, string>("TEST_95", "R4"),
                        Tuple.Create<string, string>("OPT_FLAG", "B1"),
                        Tuple.Create<string, string>("TEST_MIN", "R4"),
                        Tuple.Create<string, string>("TEST_MAX", "R4"),
                        Tuple.Create<string, string>("TST_MEAN", "R4"),
                        Tuple.Create<string, string>("TST_SDEV", "R4"),
                        Tuple.Create<string, string>("TST_SUMS", "R4"),
                        Tuple.Create<string, string>("TST_SQRS", "R4"),
                        Tuple.Create<string, string>("TEST_NAM", "Cn"),
                        Tuple.Create<string, string>("SEQ_NAME", "Cn")
                    }
                },
                {
                    "Gtr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEXT_NAME", "C16"),
                        Tuple.Create<string, string>("TEXT_VAL", "Cn")
                    }
                },
                {
                    "Adr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("CPU_TYPE", "U1"),
                        Tuple.Create<string, string>("STDF_VER", "Cn"),
                        Tuple.Create<string, string>("DB_ID", "U1"),
                        Tuple.Create<string, string>("PARA_CNT", "U2"),
                        Tuple.Create<string, string>("LOT_FLG", "U1"),
                        Tuple.Create<string, string>("RTST_CNT", "U2"),
                        Tuple.Create<string, string>("LOT_TYPE", "C1"),
                        Tuple.Create<string, string>("RTST_WAF", "K5Cn"),
                        Tuple.Create<string, string>("RTST_BIN", "K5U4")
                    }
                },
                {
                    "Epdr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("TEST_NUM", "U4"),
                        Tuple.Create<string, string>("OPT_FLAG", "B1"),
                        Tuple.Create<string, string>("CAT", "C2"),
                        Tuple.Create<string, string>("TARGET", "R4"),
                        Tuple.Create<string, string>("SPC_FLAG", "C2"),
                        Tuple.Create<string, string>("LVL", "R4"),
                        Tuple.Create<string, string>("HVL", "R4"),
                        Tuple.Create<string, string>("TEST_NAM", "Cn")
                    }
                },
                {
                    "Gdr", new List<Tuple<string, string>>{
                        Tuple.Create<string, string>("FLD_CNT", "U2"),
                        Tuple.Create<string, string>("GEN_DATA", "K0Vn")
                    }
                },
                {
                    "Shb", new List<Tuple<string, string>>{ }
                },
                {
                    "Ssb", new List<Tuple<string, string>>{ }
                },
                {
                    "Sts", new List<Tuple<string, string>>{ }
                },
                {
                    "Scr", new List<Tuple<string, string>>{ }
                },
                {
                    "Bps", new List<Tuple<string, string>>{ Tuple.Create<string, string>("SEQ_NAME", "Cn") }
                },
                {
                    "Eps", new List<Tuple<string, string>>{ }
                },
                {
                    "Dtr", new List<Tuple<string, string>>{ Tuple.Create<string, string>("TEXT_DAT", "Cn") }
                }
            };
        }
    }
}

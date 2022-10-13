using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class CauHoiThi
    {
        public CauHoiThi()
        {
            CauTraLois = new HashSet<CauTraLoi>();
        }

        public int Stt { get; set; }
        public string MaPhong { get; set; } = null!;
        public string CauHoi { get; set; } = null!;
        public string LoiGiai { get; set; } = null!;
        public string DapAn { get; set; } = null!;

        public virtual PhongThi MaPhongNavigation { get; set; } = null!;
        public virtual ICollection<CauTraLoi> CauTraLois { get; set; }

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        public bool isMultiAns(string lg)
        {
            string[] loigiai = new string[] { "" };
            loigiai = lg.Split('\\');
            if (loigiai.Length > 1) return true;
            return false;
        }
        public string getDapAnDung(string da, string lg)
        {
            string[] dapan = new string[] { "" };
            string[] loigiai = new string[] { "" };
            dapan = da.Split('\\');
            loigiai = lg.Split('\\');
            string result = "";

            for (int i = 0; i < loigiai.Length; i++)
            {
                for (int j = 0; j < dapan.Length; j++)
                {
                    if (loigiai[i].Equals(dapan[j]))
                    {
                        switch (j)
                        {
                            case 0:
                                result += ",A";
                                break;
                            case 1:
                                result += ",B";
                                break;
                            case 2:
                                result += ",C";
                                break;
                            case 3:
                                result += ",D";
                                break;
                        }
                    }
                }
            }
            return result.Substring(1);
        }
        public string getDapAnDungAsInt(string da, string lg)
        {
            string[] dapan = new string[] { "" };
            string[] loigiai = new string[] { "" };
            dapan = da.Split('\\');
            loigiai = lg.Split('\\');
            string result = "";

            for (int i = 0; i < loigiai.Length; i++)
            {
                for (int j = 0; j < dapan.Length; j++)
                {
                    if (loigiai[i].Equals(dapan[j]))
                    {
                        switch (j)
                        {
                            case 0:
                                result += ",0";
                                break;
                            case 1:
                                result += ",1";
                                break;
                            case 2:
                                result += ",2";
                                break;
                            case 3:
                                result += ",3";
                                break;
                        }
                    }
                }
            }
            return result.Substring(1);
        }

        public string getKyHieu(int vitri)
        {
            string result = "";
            switch (vitri)
            {
                case 0:
                    result += "A";
                    break;
                case 1:
                    result += "B";
                    break;
                case 2:
                    result += "C";
                    break;
                case 3:
                    result += "D";
                    break;
            }

            return result;
        }

        public bool traLoiDung(string maND, int lanthu)
        {
            var traloi = db.CauTraLois.FirstOrDefault(x => x.Stt == this.Stt && x.MaPhong == this.MaPhong && x.MaNd == maND && x.LanThu == lanthu);
            if (traloi != null && traloi.DapAn.Equals(this.LoiGiai)) return true;

            return false;
        }
    }
}

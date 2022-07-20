using System;
using System.Collections.Generic;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class TinNhan
    {
        public int Id { get; set; }
        public string? NguoiGui { get; set; }
        public string? NguoiNhan { get; set; }
        public DateTime ThoiGian { get; set; }
        public string NoiDung { get; set; } = null!;
        public bool TrangThai { get; set; }

        public virtual NguoiDung? NguoiGuiNavigation { get; set; }
        public virtual NguoiDung? NguoiNhanNavigation { get; set; }

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();

        public int setID()
        {
            var tn = db.TinNhans.OrderByDescending(x => x.Id).FirstOrDefault();
            return tn.Id + 1;
        }
        public List<TinNhan> getTinNhanChuaXem(string maND)
        {
            var tn = db.TinNhans.Where(x => x.NguoiNhan == maND && x.TrangThai == false).ToList();
            List<TinNhan> list = new List<TinNhan>();
            for (int i = 0; i < tn.Count; i++)
            {
                list.Add(tn[i]);
                if (i > 0 && tn[i - 1].NguoiGui == tn[i].NguoiGui)
                {
                    list.Remove(tn[i - 1]);
                }
            }

            return list;
        }
        public int getSLTinNhanChuaXem(string maND)
        {
            var tn = db.TinNhans.Where(x => x.NguoiNhan == maND && x.TrangThai == false).ToList();
            int dem = 0;
            for (var i = 0; i < tn.Count; i++)
            {
                dem++;
                if (i > 0 && tn[i - 1].NguoiGui == tn[i].NguoiGui)
                {
                    dem--;
                }
            }

            return dem;
        }
        public NguoiDung getUser(string maND)
        {
            var nd = db.NguoiDungs.FirstOrDefault(x => x.MaNd == maND);
            nd.Sdt = null;
            nd.Email = null;
            nd.MatKhau = null;
            nd.ImgNhanDien = null;

            return nd;
        }
        public List<TinNhan> getTinNhanTuUser(string nguoigui, string nguoinhan)
        {
            var tn = db.TinNhans.Where(x => x.NguoiGui == nguoigui && x.NguoiNhan == nguoinhan).OrderBy(x => x.ThoiGian);
            return tn.ToList();
        }
        public List<TinNhan> getAllTinNhan(string user1, string user2)
        {
            List<TinNhan> tinNhans = new List<TinNhan>();
            var tn1 = db.TinNhans.Where(x => x.NguoiGui == user1 && x.NguoiNhan == user2);
            var tn2 = db.TinNhans.Where(x => x.NguoiGui == user2 && x.NguoiNhan == user1);
            tinNhans.AddRange(tn1);
            tinNhans.AddRange(tn2);

            return tinNhans.OrderBy(x => x.ThoiGian).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DayHocTrucTuyen.Models.Entities
{
    public partial class PhongThi
    {
        public PhongThi()
        {
            BiCamThis = new HashSet<BiCamThi>();
            CauHoiThis = new HashSet<CauHoiThi>();
            ThoiGianLamBais = new HashSet<ThoiGianLamBai>();
        }

        public string MaPhong { get; set; } = null!;
        public string? MaLop { get; set; }
        public string TenPhong { get; set; } = null!;
        public DateTime NgayTao { get; set; }
        public string? MatKhau { get; set; }
        public DateTime NgayMo { get; set; }
        public DateTime NgayDong { get; set; }
        public int LuotThi { get; set; }
        public bool XemLai { get; set; }
        public int ThoiLuong { get; set; }

        public virtual LopHoc? MaLopNavigation { get; set; }
        public virtual ICollection<BiCamThi> BiCamThis { get; set; }
        public virtual ICollection<CauHoiThi> CauHoiThis { get; set; }
        public virtual ICollection<ThoiGianLamBai> ThoiGianLamBais { get; set; }

        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();
        public string setMa(string maLop)
        {
            var last = (from b in db.PhongThis
                        where b.MaLop == maLop
                        orderby b.MaPhong descending
                        select b).FirstOrDefault();
            if (last == null)
            {
                return maLop + "-001";
            }
            int temp = int.Parse(Convert.ToString(last.MaPhong).Substring(12));
            string ma = maLop + "-" + Convert.ToString(1000 + temp + 1).Substring(1);
            return ma;
        }
        public string mahoaMatKhau(string pass)
        {
            MD5 mh = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pass);
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public int getSLCauHoi()
        {
            return db.CauHoiThis.Where(x => x.MaPhong == this.MaPhong).Count();
        }
        public int getSLThi(string maND)
        {
            return db.ThoiGianLamBais.Where(x => x.MaNd == maND && x.MaPhong == this.MaPhong).Count();
        }
        public List<ThoiGianLamBai> getListThi(string maND)
        {
            return db.ThoiGianLamBais.Where(x => x.MaNd == maND && x.MaPhong == this.MaPhong).ToList();
        }
        public int getSLLamBai()
        {
            return db.ThoiGianLamBais.Where(x => x.MaPhong == this.MaPhong).Count();
        }
        public List<CauHoiThi> getAllCauHoi()
        {
            return db.CauHoiThis.Where(x => x.MaPhong == this.MaPhong).ToList();
        }
        public bool daChonDapAn(int stt, string maND, int lanthu)
        {
            var tl = db.CauTraLois.FirstOrDefault(x => x.Stt == stt && x.MaPhong == this.MaPhong && x.MaNd == maND && x.LanThu == lanthu);
            if (tl == null) return false;

            return true;
        }
        public string getDapAnDaChon(int stt, string maND, int lanthu)
        {
            var tl = db.CauTraLois.FirstOrDefault(x => x.Stt == stt && x.MaPhong == this.MaPhong && x.MaNd == maND && x.LanThu == lanthu);
            if (tl != null) return tl.DapAn;
            return "";
        }
        public int getDiemThi(string maND, int lanthu)
        {
            int diem = 0;
            for (int i = 1; i <= this.getSLCauHoi(); i++)
            {
                var cauhoi = db.CauHoiThis.FirstOrDefault(x => x.Stt == i && x.MaPhong == this.MaPhong);
                var traloi = db.CauTraLois.FirstOrDefault(x => x.Stt == i && x.MaPhong == this.MaPhong && x.MaNd == maND && x.LanThu == lanthu);
                if (cauhoi != null && traloi != null && traloi.DapAn.Equals(cauhoi.LoiGiai))
                {
                    diem++;
                }
            }
            return diem;
        }
        public List<PhongThi> searchPhongThi(string maND, string tenphong)
        {
            var phongthi = from pt in db.PhongThis
                           join lh in db.LopHocs on pt.MaLop equals lh.MaLop
                           where lh.MaNd == maND && pt.TenPhong.Contains(tenphong)
                           orderby pt.NgayTao descending
                           select pt;
            return phongthi.ToList();
        }
    }
}

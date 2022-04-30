using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using haberciyiz.Models;
using haberciyiz.ViewModel;

namespace haberciyiz.Controllers
{
    public class ServisController : ApiController
    {
        DB1Entities1 db = new DB1Entities1();

        SonucModel sonuc = new SonucModel();

        [HttpGet]
        [Route("api/Haberlerliste")]
        public List<HaberModel> HaberlerListe()
        {
            List<HaberModel> liste = db.Haberler.Select(x => new HaberModel()
            {
                HaberId = x.HaberId,
                HaberKatId = x.HaberKatId,
                HaberBaslik = x.HaberBaslik,
                HaberAciklama = x.HaberAciklama,
                EditorId = x.EditorId,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/haberbyid/{HaberId}")]
        public HaberModel HaberlerById(String HaberId)
        {
            HaberModel kayit = db.Haberler.Where(s => s.HaberId == HaberId).Select(x => new HaberModel()
            {
                HaberId = x.HaberId,
                HaberKatId = x.HaberKatId,
                HaberBaslik = x.HaberBaslik,
                HaberAciklama = x.HaberAciklama,
                EditorId = x.EditorId,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/haberekle")]
        public SonucModel HaberlerEkle(HaberModel model)
        {
            if (db.Haberler.Count(c => c.HaberKatId == model.HaberKatId) > 0)
            {
                sonuc.islem = false;
                sonuc.Mesaj = "Girilen Haber Kayıtlıdır!";
                return sonuc;
            }

            Haberler yeni = new Haberler();
            yeni.HaberId = Guid.NewGuid().ToString();
            yeni.HaberKatId = model.HaberKatId;
            yeni.HaberBaslik = model.HaberBaslik;
            yeni.HaberAciklama = model.HaberAciklama;
            yeni.EditorId = model.EditorId;
            db.Haberler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.Mesaj = "Haber Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/haberduzenle")]
        public SonucModel HaberlerDuzenle(HaberModel model)
        {
            Haberler kayit = db.Haberler.Where(s => s.HaberId == model.HaberId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.Mesaj = "Kayıt Bulunmadı!";
                return sonuc;
            }

            kayit.HaberKatId = model.HaberKatId;
            kayit.HaberBaslik = model.HaberBaslik;
            kayit.HaberAciklama = model.HaberAciklama;
            kayit.EditorId= model.EditorId;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.Mesaj = "Haber Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/habersil/{HaberId}")]
        public SonucModel HaberlerSil(string HaberId)
        {
            Haberler kayit = db.Haberler.Where(s => s.HaberId == HaberId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.Mesaj = "Kayıt Bulunmadı!";
                return sonuc;
            }

            if (db.Kayit.Count(c => c.KayitHaberId == HaberId) > 0)
            {
                sonuc.islem = false;
                sonuc.Mesaj = "Kayıtlı Haberler Silinemez!";
                return sonuc;
            }


            db.Haberler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.Mesaj = "Haber Silndi";
            return sonuc;
        }
        [HttpGet]
        [Route("api/kategorilerliste")]
        public List<KategorilerModel> KategoriListe()
        {


            {
                List<KategorilerModel> liste = db.Kategoriler.Select(x => new KategorilerModel()
                {
                    KatId = x.KatId,
                    KatAdi = x.KatAdi,
                }).ToList();

                return liste;
            }
        }
            [HttpGet]
            [Route("api/kategoribyid/{kategoriId}")]

            public KategorilerModel KategorilerById(string KatId)
        {
            KategorilerModel kayit = db.Kategoriler.Where(s => s.KatId == KatId).Select(x => new KategorilerModel()
            {
                KatId = x.KatId,
                KatAdi = x.KatAdi,
            }).SingleOrDefault();
            return kayit;
        }

      [HttpPost]
      [Route("api/Kategoriekle")]
    public SonucModel KategoriEkle(KategorilerModel model)
    {
        if (db.Kategoriler.Count(c => c.KatId == model.KatId) > 0)
        {
            sonuc.islem = false;
            sonuc.Mesaj = "Girilen Kategori Kayıtlıdır!";
            return sonuc;
        }

        Kategoriler yeni = new Kategoriler();
        yeni.KatId = Guid.NewGuid().ToString();
        yeni.KatAdi = model.KatAdi;
        db.Kategoriler.Add(yeni);
        db.SaveChanges();
        sonuc.islem = true;
        sonuc.Mesaj = "Kategori Eklendi";

        return sonuc;
    }
    [HttpPut]
    [Route("api/Kategoriduzenle")]
    public SonucModel KategorilerDuzenle(KategorilerModel model)
    {
        Kategoriler kayit = db.Kategoriler.Where(s => s.KatId == model.KatId).SingleOrDefault();

        if (kayit == null)
        {
            sonuc.islem = false;
            sonuc.Mesaj = "Kayıt Bulunamadı!";
            return sonuc;
        }


        kayit.KatAdi = model.KatAdi;

        db.SaveChanges();
        sonuc.islem = true;
        sonuc.Mesaj = "Kategori Düzenlendi";

        return sonuc;
    }
    [HttpDelete]
    [Route("api/Kategorisil/{KatId}")]
    public SonucModel KategorilerSil(string KatId)
    {
        Kategoriler kayit = db.Kategoriler.Where(s => s.KatId == KatId).SingleOrDefault();

        if (kayit == null)
        {
            sonuc.islem = false;
            sonuc.Mesaj = "Kayıt Bulunamadı!";
            return sonuc;
        }

        if (db.Kayit.Count(c => c.KayitKategoriId == KatId) > 0)
        {
            sonuc.islem = false;
            sonuc.Mesaj = "Baş Kategori Silinmez!";
            return sonuc;
        }

        db.Kategoriler.Remove(kayit);
        db.SaveChanges();
        sonuc.islem = true;
        sonuc.Mesaj = "Kategori Silindi";

        return sonuc;
    }
    [HttpPost]
    [Route("api/kayitekle")]
    public SonucModel KayitEkle(KayitModel model)
    {
        if (db.Kayit.Count(c => c.KayitId == model.KayitId & c.KayitHaberId == model.KayitHaberId) > 0)
        {
            sonuc.islem = false;
            sonuc.Mesaj = "Öğrenci Ders Kaydı Önceden Yapılmıştır!";
            return sonuc;
        }

        Kayit yeni = new Kayit();
        yeni.KayitId = Guid.NewGuid().ToString();
        yeni.KayitHaberId = model.KayitHaberId;
        yeni.KayitKategoriId = model.KayitKategoriId;
        db.Kayit.Add(yeni);
        db.SaveChanges();
        sonuc.islem = true;
        sonuc.Mesaj = "Kayıt Eklendi";
        return sonuc;
    }

    [HttpDelete]
    [Route("api/kayitsil/{kayitId}")]
    public SonucModel KayitSil(string KayitId)
    {
        Kayit kayit = db.Kayit.Where(s => s.KayitId == KayitId).SingleOrDefault();

        if (kayit == null)
        {
            sonuc.islem = false;
            sonuc.Mesaj = "Kayıt Bulunamadı!";
            return sonuc;
        }


        db.Kayit.Remove(kayit);
        db.SaveChanges();
        sonuc.islem = true;
        sonuc.Mesaj = "Kayıt Silindi";

        return sonuc;
    }

}
}


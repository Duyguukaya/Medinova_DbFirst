# 🏥 MEDİNOVA – Yapay Zeka Destekli Çok Panelli Hastane Yönetim Sistemi

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![ASP.NET](https://img.shields.io/badge/ASP.NET-MVC%205-purple.svg)
![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-blueviolet.svg)
![OpenAI](https://img.shields.io/badge/AI-OpenAI%20GPT--4-green.svg)
![Status](https://img.shields.io/badge/Status-Completed-success.svg)

**MEDİNOVA**; hastaların randevu oluşturduğu, doktorların günlük programlarını takip ettiği ve yöneticilerin tüm sistemi kontrol ettiği; modern, kullanıcı dostu ve **Yapay Zeka (OpenAI) destekli** bir hastane yönetim sistemidir.

Proje; **ASP.NET MVC 5**, **Entity Framework Database-First**, **Generic Repository Pattern** ve **3 farklı kullanıcı paneli** kullanılarak kurumsal standartlarda geliştirilmiştir.

---

## 🤖 Öne Çıkan Özellik: Akıllı Tanı & Bölüm Önerisi (OpenAI)

MEDİNOVA'yı diğer sistemlerden ayıran en büyük fark, bünyesindeki **OpenAI API (GPT-4 / GPT-3.5 Turbo)** entegrasyonudur. Bu özellik sayesinde hastalar, "Hangi bölüme gitmeliyim?" kararsızlığını yaşamazlar.

* **Akıllı Şikayet Analizi:** Hasta, tıbbi bir terim bilmek zorunda kalmadan sadece şikayetini (örn: "Gözlerimde aşırı yanma ve kaşıntı var") doğal dilde yazar.
* **Otomatik Bölüm Yönlendirme:** Yapay zeka, metni gerçek zamanlı analiz ederek en uygun tıbbi birimi (örn: Göz Hastalıkları) belirler ve randevu formundaki "Bölüm" alanını otomatik olarak seçer.
* **Hızlı Doktor Eşleşmesi:** Analiz sonucu önerilen bölüme ait aktif doktorlar AJAX ile anında listelenir, böylece hastanın yanlış birimden randevu alması engellenir.

---

## 🚀 Proje Hakkında ve Öne Çıkanlar

* **🛡️ Rol Tabanlı Yetkilendirme:** Admin, Doctor ve Patient rolleri tamamen birbirinden izole panellere yönlendirilir.
* **📅 Akıllı Randevu Sistemi:** Bölüm → Doktor → Tarih → Saat sıralamasıyla çalışan AJAX destekli randevu formu; dolu saatleri gerçek zamanlı gösterir.
* **🔔 Randevu Hatırlatıcı:** Hasta paneline giriş yapıldığında 7 gün içindeki randevular için renkli bildirimler gösterilir (Bugün, Yarın, X gün kaldı).
* **⚙️ Dinamik Anasayfa:** Banner, Hakkımızda, Doktorlar ve Referanslar bölümleri Admin panelinden dinamik olarak yönetilir.
* **📊 Admin Dashboard:** Doktor, randevu, bölüm ve kullanıcı sayılarını tek ekranda özetleyen gelişmiş istatistik kartları.

---

## 👥 Panel Yapısı ve Özellikler

### 🛡️ 1. Admin Paneli
* **Dashboard:** Hastanenin genel doluluk ve kullanıcı verilerini özetleyen ekran.
* **Doktor & Bölüm Yönetimi:** Tam CRUD işlemleri ve doktor randevu listesi görüntüleme.
* **Randevu Yönetimi:** Tüm randevuları listeleme, durum güncelleme (Aktif/Pasif) ve silme.
* **İçerik Yönetimi:** Web sitesindeki görselleri ve metinleri veritabanı üzerinden anlık güncelleme.

### 🩺 2. Doktor Paneli
* **Panel Özeti:** Bugünkü ve yaklaşan randevu sayılarını gösteren istatistikler.
* **İş Akışı:** Bugünün randevularını saate göre sıralı, geçmiş randevuları ise tarihe göre azalan sırada listeleme.

### 👤 3. Hasta Paneli
* **Randevu Takibi:** Aktif, tamamlandı ve iptal edildi durumlarını renkli badge'lerle izleme.
* **İptal Mekanizması:** Tarihi geçmemiş aktif randevuları tek tıkla iptal edebilme.

###  4. Ana Sayfa Paneli,
* **AI Desteği:** Şikayetini yazarak kendisine en uygun bölümün önerilmesini sağlayan arayüz.

---

## 🛠️ Teknik Mimari ve Kullanılan Teknolojiler

| Alan | Teknoloji / Araç |
| :--- | :--- |
| **Framework** | ASP.NET MVC 5 (.NET Framework 4.8) |
| **Yapay Zeka** | OpenAI API (GPT-4 / GPT-3.5 Turbo) |
| **Veritabanı & ORM** | MS SQL Server, Entity Framework 6 (Database First) |
| **Mimari Desenler** | Generic Repository Pattern, Areas, N-Layered Architecture |
| **UI & Frontend** | Bootstrap 4, jQuery, AJAX, FontAwesome 5, SB Admin 2 |
| **Mapping** | AutoMapper |

---

## 🗂️ Proje Mimarisi

```
Medinova/
│
├── Areas/
│   ├── Admin/
│   │   ├── Controllers/          # 9 Controller (CRUD + Dashboard)
│   │   └── Views/                # Her entity için Index/Create/Edit/Delete
│   ├── Doctor/
│   │   ├── Controllers/          # DoctorDashboardController
│   │   └── Views/                # Index, TodayAppointments, MyAppointments
│   └── Patient/
│       ├── Controllers/          # PatientPanelController
│       └── Views/                # Index (bildirimli), NewAppointment
│
├── Controllers/
│   ├── AccountController.cs      # Login, Logout, rol yönlendirme
│   └── DefaultController.cs      # Dinamik anasayfa partial action'ları
│
├── Models/                       # EF Database-First modeller
│   ├── Appointment.cs
│   ├── Doctor.cs
│   ├── Department.cs
│   ├── User.cs
│   ├── Role.cs
│   ├── Banner.cs
│   ├── About.cs
│   ├── AboutItem.cs
│   ├── Testimonial.cs
│   └── MedinovaContext.cs
│
├── Repositories/                 # Generic Repository Pattern
│   ├── IGenericRepository.cs
│   ├── GenericRepository.cs
│   └── AppointmentRepositories/
│
├── Filters/
│   └── RoleAuthorize.cs          # Özel yetkilendirme attribute'u
│
├── Enums/
│   └── Times.cs                  # Randevu saatleri listesi
│
├── Views/
│   ├── Default/                  # Anasayfa partial view'ları
│   │   ├── Index.cshtml
│   │   ├── _Hero.cshtml
│   │   ├── _About.cshtml
│   │   ├── _Doctors.cshtml
│   │   ├── _Testimonials.cshtml
│   │   └── DefaultAppointment.cshtml
│   └── Shared/
│       ├── _UILayout.cshtml      # Genel web sitesi layout'u
│       └── _AdminLayout.cshtml   # Admin panel layout'u
│
└── Templates/                    # CSS/JS şablonları
    ├── Medinova-1.0.0/           # Genel web sitesi teması
    └── startbootstrap-sb-admin-2/ # Admin panel teması
```

---

## 🔐 Kullanıcı Rolleri ve Yönlendirme

Giriş yapıldığında kullanıcının rolüne göre otomatik yönlendirme yapılır:

| Rol | Yönlendirme | Erişim |
| :--- | :--- | :--- |
| **Admin** | `/Admin/Dashboard/Index` | Tüm paneller ve CRUD işlemleri |
| **Doctor** | `/Doctor/DoctorDashboard/Index` | Sadece kendi randevuları |
| **Patient** | `/Patient/PatientPanel/Index` | Randevu oluşturma ve takip |
| **Yetkisiz** | `/Account/Login` | Giriş sayfası |

---

## 📅 Randevu Akışı

```
1. Hasta bölüm seçer
        ↓ (AJAX)
2. O bölümdeki doktorlar yüklenir
        ↓
3. Doktor ve tarih seçilir
        ↓ (AJAX)
4. Dolu/boş saatler renk kodlu gösterilir
        ↓
5. Form gönderilir → Server'da çakışma kontrolü yapılır
        ↓
6. Randevu oluşturulur (IsActive = true)
        ↓
7. Hasta panelinde bildirim gösterilir
```

📅 Randevu Akışı (AI Destekli)

Giriş: Hasta şikayetini metin olarak girer.

AI Analizi: OpenAI API metni analiz eder ve en uygun Bölüm bilgisini döner.

Otomatik Filtre: Sistem, AJAX ile önerilen bölümü seçer ve o bölümdeki doktorları listeler.

Kontrol: Seçilen tarih ve doktora göre dolu saatler "(Dolu)" etiketiyle pasif gösterilir.

Kayıt: Randevu oluşturulur ve hasta panelinde "X gün kaldı" bildirimi tetiklenir.



## 📷 Ekran Görüntüleri

### Genel Web Sitesi

<!-- Anasayfa görseli buraya eklenecek -->

<!-- Randevu formu görseli buraya eklenecek -->

<!-- Doktorlar carousel görseli buraya eklenecek -->

### Admin Paneli

<!-- Dashboard görseli buraya eklenecek -->

<!-- Doktor yönetimi görseli buraya eklenecek -->

<!-- Randevu yönetimi görseli buraya eklenecek -->

### Doktor Paneli

<!-- Doktor dashboard görseli buraya eklenecek -->

<!-- Bugünün randevuları görseli buraya eklenecek -->

### Hasta Paneli

<!-- Hasta randevu listesi görseli buraya eklenecek -->

<!-- Randevu bildirimleri görseli buraya eklenecek -->

<!-- Yeni randevu oluşturma görseli buraya eklenecek -->



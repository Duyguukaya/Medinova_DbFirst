# 🏥 MEDİNOVA – Çok Panelli Hastane Yönetim Sistemi

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![ASP.NET](https://img.shields.io/badge/ASP.NET-MVC%205-purple.svg)
![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-blueviolet.svg)
![Status](https://img.shields.io/badge/Status-Completed-success.svg)
![EF](https://img.shields.io/badge/Entity%20Framework-Database%20First-orange.svg)

**MEDİNOVA**; hastaların randevu oluşturduğu, doktorların günlük programlarını takip ettiği ve yöneticilerin tüm sistemi kontrol ettiği; modern ve kullanıcı dostu bir **hastane yönetim sistemidir.**

Proje; **ASP.NET MVC 5**, **Entity Framework Database-First**, **Generic Repository Pattern** ve **3 farklı kullanıcı paneli** kullanılarak kurumsal standartlarda geliştirilmiştir.

---

## 🚀 Proje Hakkında ve Öne Çıkanlar

MEDİNOVA, klasik hastane web sitelerinden farklı olarak her kullanıcı tipine özel izole panel sunar ve randevu sürecini uçtan uca yönetir.

* **🛡️ Rol Tabanlı Yetkilendirme:** Admin, Doctor ve Patient rolleri tamamen birbirinden izole panellere yönlendirilir. Yetkisiz erişimler otomatik olarak Login sayfasına yönlendirilir.
* **📅 Akıllı Randevu Sistemi:** Bölüm → Doktor → Tarih → Saat sıralamasıyla çalışan AJAX destekli randevu formu; dolu saatleri gerçek zamanlı gösterir, çakışmaları engeller.
* **🔔 Randevu Hatırlatıcı:** Hasta paneline giriş yapıldığında 7 gün içindeki randevular için renkli bildirimler gösterilir (bugün, yarın, X gün kaldı).
* **⚙️ Dinamik Anasayfa:** Banner, Hakkımızda, Doktorlar ve Referanslar bölümleri veritabanından dinamik olarak çekilir; içerik boşsa hata vermez, varsayılan değerler gösterilir.
* **📊 Admin Dashboard:** Doktor, randevu, bölüm ve kullanıcı sayılarını tek ekranda özetler.

---

## 👥 Panel Yapısı ve Özellikler

Sistemde **Admin, Doctor ve Patient** olmak üzere birbirinden izole 3 farklı panel bulunmaktadır.

### 🛡️ 1. Admin Paneli
Tam yetkili yönetim merkezidir.
* **Dashboard & İstatistikler:** Doktor, randevu, bölüm ve kullanıcı sayılarını gösteren özet ekran.
* **Doktor Yönetimi:** Doktor ekleme, düzenleme, silme ve her doktora ait randevu listesini görüntüleme.
* **Randevu Yönetimi:** Tüm randevuları listeleme, düzenleme, silme ve aktif/pasif durumunu değiştirme.
* **Bölüm Yönetimi:** Hastane bölümlerini tam CRUD işlemleriyle yönetme.
* **Kullanıcı & Rol Yönetimi:** Kullanıcı oluşturma, düzenleme, silme ve checkbox ile rol atama.
* **İçerik Yönetimi:** Banner, Hakkımızda, Hakkımızda Maddeleri ve Referanslar için tam CRUD.

### 🩺 2. Doktor Paneli
İçerik üreticileri için özel alandır.
* **Panel Özeti:** Bugünkü, yaklaşan ve toplam randevu sayılarını gösteren istatistik kartları.
* **Bugünün Randevuları:** O güne ait randevuları saate göre sıralı listeler.
* **Tüm Randevularım:** Geçmiş ve gelecek tüm randevuları tarihe göre azalan sırada listeler.
* **Profil:** Oturum açmış doktorun adı topbar'da ve sidebar'da gösterilir.

### 👤 3. Hasta Paneli
Son kullanıcı deneyim alanıdır.
* **Randevu Takibi:** Hastanın tüm randevularını aktif, tamamlandı ve iptal edildi olarak renkli badge'lerle gösterir.
* **Randevu Oluşturma:** Bölüm seçince AJAX ile doktorlar yüklenir; doktor ve tarih seçince dolu/boş saatler renk kodlu gösterilir.
* **Randevu İptali:** Tarihi geçmemiş aktif randevular tek tıkla iptal edilebilir; geçmiş randevular iptal edilemez.
* **Akıllı Bildirimler:** Giriş yapıldığında 7 gün içindeki randevular için uyarı gösterilir. Tablo satırlarında "Bugün!", "Yarın!", "X gün kaldı" ve "Günü Geçti" badge'leri bulunur.

---

## 🌐 Genel Web Sitesi Özellikleri

* **Dinamik Hero Section:** Banner tablosundan başlık, açıklama ve arka plan görseli çekilir.
* **Dinamik Hakkımızda:** About tablosundan içerik, AboutItems tablosundan ikonlu maddeler gösterilir.
* **Doktor Carousel:** Tüm doktorlar bölüm bilgileriyle birlikte owl carousel'de listelenir.
* **Referanslar:** Sadece onaylı (IsApproved = true) referanslar görüntülenir.
* **AJAX Randevu Formu:** Bölüm → Doktor → Tarih → Saat akışı; dolu saatler "(Dolu)" etiketiyle pasif gösterilir.
* **Türkçe Arayüz:** Navbar, footer, form alanları ve tüm içerikler Türkçedir.

---

## 🛠️ Teknik Mimari ve Kullanılan Teknolojiler

Proje **Repository Pattern** kullanılarak **N-Katmanlı** bir yapıda geliştirilmiştir.

| Alan | Teknoloji / Araç |
| :--- | :--- |
| **Framework** | ASP.NET MVC 5 |
| **Platform** | .NET Framework 4.8 |
| **Veritabanı & ORM** | MS SQL Server, Entity Framework 6 (Database First) |
| **Mimari Desenler** | Generic Repository Pattern, Areas (Admin/Doctor/Patient) |
| **Kimlik Doğrulama** | FormsAuthentication, Session tabanlı rol yönetimi |
| **Mapping** | AutoMapper |
| **UI & Frontend** | Bootstrap 4, jQuery, FontAwesome 5, SB Admin 2 |
| **UI Yapısı** | Areas, Partial Views, ChildActionOnly, Layouts |
| **AJAX** | jQuery AJAX (doktor filtreleme, saat kontrolü) |
| **Veritabanı Tasarımı** | Database First, EDMX Model |

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

---

## ⚙️ Kurulum

### Gereksinimler
* Visual Studio 2022
* .NET Framework 4.8
* MS SQL Server (LocalDB veya Express)

### Adımlar

```bash
# 1. Projeyi klonlayın
git clone https://github.com/kullanici-adi/medinova.git

# 2. Visual Studio'da açın
# Medinova.sln dosyasına çift tıklayın

# 3. Web.config dosyasında bağlantı dizesini güncelleyin
# data source=SUNUCU_ADINIZ\SQLEXPRESS; initial catalog=MedinovaDb

# 4. SQL Server'da MedinovaDb veritabanını oluşturun

# 5. seed_data.sql dosyasını SSMS'de çalıştırın
# (Örnek veriler: doktorlar, bölümler, kullanıcılar, randevular)

# 6. Projeyi çalıştırın (F5)
```

### Varsayılan Giriş Bilgileri

| Kullanıcı Adı | Şifre | Rol |
| :--- | :--- | :--- |
| `admin` | `123456` | Admin |
| `ahmet.yildiz` | `123456` | Doctor |
| `ayse.gunes` | `123456` | Doctor |
| `hilal` | `123456` | Patient |
| `ali.veli` | `123456` | Patient |

---

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

---

## 🗃️ Veritabanı Şeması

```
Users ──────────── RoleUser ──────── Roles
  │
  └── (email ile) Appointments
                      │
                   Doctors ──────── Departments
                   
Banners
Abouts ──────── AboutItems
Testimonials
```

**Tablolar:**

| Tablo | Açıklama |
| :--- | :--- |
| `Users` | Sistem kullanıcıları |
| `Roles` | Admin, Doctor, Patient rolleri |
| `RoleUser` | Kullanıcı-rol ilişki tablosu |
| `Doctors` | Doktor bilgileri |
| `Departments` | Hastane bölümleri |
| `Appointments` | Randevu kayıtları |
| `Banners` | Anasayfa hero görseli ve metni |
| `Abouts` | Hakkımızda içeriği |
| `AboutItems` | Hakkımızda ikon maddeleri |
| `Testimonials` | Hasta referansları |

---

## 📋 Özellik Listesi

- [x] Rol tabanlı yetkilendirme (Admin / Doctor / Patient)
- [x] Admin — Doktor CRUD + doktor bazlı randevu görüntüleme
- [x] Admin — Randevu yönetimi (aktif/pasif toggle)
- [x] Admin — Bölüm, Banner, Hakkımızda, Referans CRUD
- [x] Admin — Kullanıcı oluşturma + rol atama
- [x] Admin — Dashboard istatistikleri
- [x] Doktor — Bugünkü randevuları görüntüleme
- [x] Doktor — Tüm randevu geçmişi
- [x] Hasta — AJAX destekli randevu oluşturma
- [x] Hasta — Dolu saat kontrolü (server + client)
- [x] Hasta — Randevu iptali (geçmiş randevular iptal edilemez)
- [x] Hasta — 7 günlük randevu bildirimleri
- [x] Hasta — "Günü Geçti" badge'i
- [x] Dinamik anasayfa (veritabanından çekilen içerikler)
- [x] Boş veri durumunda hata vermez, varsayılan değerler gösterilir
- [x] Türkçe arayüz

---

## 👩‍💻 Geliştirici

**Duygu Kaya**

---

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

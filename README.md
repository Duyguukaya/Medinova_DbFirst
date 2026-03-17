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


## 📷 Ekran Görüntüleri

### Genel Web Sitesi

<img width="1898" height="824" alt="Ekran görüntüsü 2026-03-17 172229" src="https://github.com/user-attachments/assets/c9041163-9b53-4060-9357-641d0d12a28c" />
<img width="1896" height="824" alt="Ekran görüntüsü 2026-03-17 172241" src="https://github.com/user-attachments/assets/17f67457-4c94-4cea-b5bc-865f002c7603" />
<img width="1895" height="824" alt="Ekran görüntüsü 2026-03-17 172257" src="https://github.com/user-attachments/assets/45a1b832-79dc-4af2-b0c9-3a3d89e3189d" />
<img width="1896" height="825" alt="Ekran görüntüsü 2026-03-17 172308" src="https://github.com/user-attachments/assets/741fb2e4-fa0a-4213-87f9-12ab33e6533a" />
<img width="1896" height="822" alt="Ekran görüntüsü 2026-03-17 172321" src="https://github.com/user-attachments/assets/79872db0-2dcf-4b7a-a58c-3111c6268fc1" />
<img width="1897" height="827" alt="Ekran görüntüsü 2026-03-17 172402" src="https://github.com/user-attachments/assets/e33a8206-432a-4cb3-8aa5-3dd76ef039bd" />
<img width="1897" height="824" alt="Ekran görüntüsü 2026-03-17 172414" src="https://github.com/user-attachments/assets/8f5ba9eb-3131-470d-94e6-6422a115824d" />

### Admin Paneli

<img width="1898" height="822" alt="Ekran görüntüsü 2026-03-17 172441" src="https://github.com/user-attachments/assets/1b11cdf5-5044-4735-9de4-f9e3f9abb8c4" />
<img width="1895" height="827" alt="Ekran görüntüsü 2026-03-17 172458" src="https://github.com/user-attachments/assets/b1217565-4c0f-438b-9ed6-a2ed8eb3d952" />
<img width="1895" height="825" alt="Ekran görüntüsü 2026-03-17 172512" src="https://github.com/user-attachments/assets/876472c7-44b9-4e8c-a931-f39f291a020c" />
<img width="1895" height="818" alt="Ekran görüntüsü 2026-03-17 172541" src="https://github.com/user-attachments/assets/b1dfec65-e3b6-4584-a1d2-106940dd8dcf" />

### Doktor Paneli

<img width="1918" height="823" alt="Ekran görüntüsü 2026-03-17 172612" src="https://github.com/user-attachments/assets/2852abc4-e94d-426a-8eea-861c421fb59f" />
<img width="1919" height="826" alt="Ekran görüntüsü 2026-03-17 172621" src="https://github.com/user-attachments/assets/5b8c571b-8e2b-4745-995d-a4e7d852cfaf" />

### Hasta Paneli

<img width="1896" height="824" alt="Ekran görüntüsü 2026-03-17 172701" src="https://github.com/user-attachments/assets/fa5752f0-61a8-4379-94da-7547520ae967" />
<img width="1896" height="821" alt="Ekran görüntüsü 2026-03-17 172726" src="https://github.com/user-attachments/assets/ab967bae-6dd3-433c-aa0b-67300f968f55" />





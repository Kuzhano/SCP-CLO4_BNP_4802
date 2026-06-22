# Sistem Manajemen Proposal (GUI Version) - Tugas Besar Konstruksi PL (CLO4)

## Overview
Repository ini berisi implementasi **Fase 2 (CLO4)** untuk Tugas Besar mata kuliah Konstruksi Perangkat Lunak. 

Melanjutkan arsitektur *backend* yang telah dibangun pada iterasi CLO2, aplikasi ini sekarang telah berevolusi menjadi sistem berbasis **Graphical User Interface (GUI)** menggunakan teknologi **Windows Forms**. 

Fase ini berfokus pada integrasi penuh antar halaman modul, penerapan *Clean Code* (pemisahan logika bisnis dari UI), injeksi *Design Pattern*, dan standardisasi *Secure Coding*, tanpa menghilangkan ketegasan *Design by Contract* (DbC) dari iterasi sebelumnya.

## Tim Pengembang & Distribusi Modul
Sistem monolitik ini dirajut dari 5 modul yang dikerjakan secara asinkron. Setiap *engineer* mengawal modulnya dari level *backend* hingga perancangan UI Windows Forms, serta memastikan konektivitas dengan modul lainnya.

| Nama | NIM | Fokus Modul | Teknik Konstruksi Utama |
| :--- | :--- | :--- | :--- |
| WAWAN SAPUTRA | 103022400098 | **Modul 1: IAM & Menu Utama** | API & Automata |
| ABDURRAHMAN SALEH | 103022430010 | **Modul 2: Pendaftaran Proposal** | Generics & Runtime Config |
| MOHAMMAD DZAKI AYATILLAH ARYA HUSEIN | 103022400090 | **Modul 3: Review & Penilaian** | Table-Driven & Automata |
| RAYKA AQIL MUMTAZ | 103022400020 | **Modul 4: Pemantauan & Dashboard** | Code Reuse & Runtime Config |
| M. RAYHAN RAMADHAN AFDHANI | 103022400107 | **Modul 5: Pengarsipan & Ekspor** | Generics & API |

## Development Stack & Arsitektur
- **Language** : `C#`
- **Framework**: `.NET Modern` (Windows Forms App - Event-Driven)
- **Database** : `Single Source of Truth JSON (proposals.json & users.json)`
- **Architecture**: Terpisah antara *Business Logic Layer* (`*Service.cs`) dan *Presentation Layer* (`Form*.cs`).
- **Testing** : `MSTest Test Project` (Unit Testing untuk validasi DbC & Logika Transisi)

## Metrik Kepatuhan CLO4 (*Compliance Checklist*)
Sebagai standar *Software Quality Assurance*, fase GUI ini mendemonstrasikan kelayakan sistem melalui metrik berikut:
- [x] **Fully Interconnected GUI**: Seluruh modul saling terhubung. Navigasi diatur secara sentral melalui `FormMenu` (Modul 1) dengan *role-based access control* (Dosen vs Admin).
- [x] **Design Pattern Implementation**: Menggunakan **Repository Pattern** (`JsonRepository<T>`) untuk menyatukan jalur I/O data, serta **Singleton Pattern** pada *Runtime Configuration*.
- [x] **Secure Coding**: Implementasi *masking* pada *password field* (`UseSystemPasswordChar`), sanitasi input via `.Trim()`, dan proteksi hak akses ekspor/review data.
- [x] **Clean Code Principles**: Logika I/O dan Automata tidak dicampur ke dalam *event handler* tombol UI (*Code-Behind*). UI murni bertugas merender *state*.
- [x] **Legacy CLO2 Fulfillment**: Tetap mempertahankan *Defensive Programming*, pembagian 2 teknik konstruksi per mahasiswa, serta Unit/Performance testing.

---
*Dokumentasi ini disiapkan untuk memenuhi standar evaluasi pada Pertemuan ke-16 (Presentasi & Demo Sistem).*

## <u> Mini Kurs Satış Sitesi Proje Ödevi</u>

<details>
<summary><strong>Ödev Detayı</strong></summary>

## Projenin Amacı
Bu proje, katmanlı mimari kullanılarak bir kurs satış sitesi geliştirilmesini amaçlar. MVC ve API entegrasyonu, JWT tabanlı kimlik doğrulama ile sağlanacak, kullanıcılar kursları görüntüleyip satın alabilecektir.

---

## Proje Gereksinimleri

### 1. Katmanlı Mimari (N-Layer Architecture veya Clean Architecture)
- **Katman Yapısı**:
  - **Data Access Layer**: Veri tabanıyla iletişimi sağlayacak.
  - **Business Logic Layer**: İş kurallarını ve veri işleme mantığını barındıracak.
  - **Presentation Layer**: Kullanıcı arayüzü (MVC).

---

### 2. MVC Uygulaması

#### Kullanıcı Arayüzü
- **Kurs Kataloğu**: Tüm kurslar, adı, açıklaması, fiyatı ve kategorisiyle listelenmeli.
- **Kurs Detayları**: Kurs hakkında detaylar ve sepete ekleme özelliği.
- **Sipariş Sayfası**: Kullanıcı sepetteki kursları görüp sipariş verebilmeli.
- **Ödeme Sayfası**: Ödeme bilgileri alınarak işlem tamamlanmalı.

#### Kimlik Doğrulama
- JWT tabanlı kimlik doğrulama, MVC ile API arasında güvenli bağlantı sağlayacak.

#### Sayfa Yapısı
1. **Kurs Kataloğu Sayfası**
   - Tüm kursları listeleme.
2. **Kurs Detay Sayfası**
   - Detay gösterimi ve sepete ekleme.
3. **Sipariş Sayfası**
   - Sepet ve satın alma işlemleri.
4. **Ödeme Sayfası**
   - Ödeme bilgilerini alarak işlemi sonlandırma.

---

### 3. API Uygulaması

#### Genel Gereksinimler
- JWT tabanlı kimlik doğrulama.
- Aşağıdaki endpoint'ler sağlanmalıdır:

#### Endpoints
1. **Catalog Endpoints**
   - Kurs listeleme ve filtreleme.
   - Kurs detaylarını sağlama.
2. **Order Endpoints**
   - Sipariş kaydetme.
   - Sipariş detaylarını görüntüleme.
   - Kullanıcının geçmiş siparişlerini görüntüleme.
3. **Payment Endpoints**
   - Ödeme işlemleri.
   - Ödemenin başarıyla tamamlandığını doğrulama.
4. **User Management Endpoints**
   - Kullanıcı oluşturma, güncelleme, silme.

---

### 4. Veritabanı ve Veri İşlemleri
- **Kurslar**: Ad, açıklama, fiyat, kategori bilgilerini içeren tablo.
- **Kullanıcılar**: Kullanıcı bilgilerini güvenli şekilde saklayan tablo.
- **Siparişler**: Kullanıcının satın aldığı kursları içeren tablo.
- **Ödemeler**: Ödeme bilgilerini içeren tablo.

#### ORM Kullanımı
- Entity Framework gibi bir ORM kullanılarak CRUD işlemleri yapılmalı.

---

### 5. JWT Tabanlı Güvenlik
- Kullanıcı giriş işlemi sonrası JWT token almalı.
- MVC uygulaması, token ile API’ye yetkili istek göndermeli.
- Yetkilendirme, her kullanıcının sadece kendi verilerine erişmesini sağlayacak şekilde yapılandırılmalı.

---

### 6. Ekstra Gereksinimler
1. **Hata Yönetimi ve Logging**
   - Kullanıcı dostu hata mesajları.
   - Hata loglarının tutulması.
2. **Validasyon**
   - Form girişleri ve API istekleri doğrulanmalı.
3. **UI/UX Geliştirme**
   - Kullanıcı dostu ve işlevsel bir arayüz tasarımı.
4. **Dokümantasyon**
   - API uç noktalarının kullanımıyla ilgili bir dokümantasyon.
</details>

***


## <u>API Endpoint Dökümantasyonu</u>

<details>
<summary><strong>Admin Controller</strong></summary>
<details>
  <summary>Kategori İşlemleri</summary>

  **1. Yeni Kategori Oluştur**  
- **Açıklama:** Yeni bir kategori oluşturur.  
- **HTTP Metodu:** `POST`  
- **URL:** `/Admin/category`  
- **Request Body:**
    ```json
    {
      "name": "Programming"
    }
    ```

**2. Kategori Güncelle**  
- **Açıklama:** Var olan bir kategoriyi günceller.  
- **HTTP Metodu:** `PUT`  
- **URL:** `/Admin/category`  
- **Request Body:**
    ```json
    {
      "id": "d8e1c9d0-465f-4d1f-bb62-c3bcfa4b0f84",
      "name": "Advanced Programming"
    }
    ```
**3. Kategori sil**  
- **Açıklama:** Belirtilen kategoriyi siler.  
- **HTTP Metodu:** `DELETE`  
- **URL:** `/Admin/category/{id}` 
- **Request Body:**
    ```json
    {
      "status": "Success",
      "message": "Category deleted successfully."   
    }
    ```
**4. Tüm Kategorileri Getir**  
- **Açıklama:** Tüm kategorileri listeler.  
- **HTTP Metodu:** `GET`  
- **URL:** `/Admin/category`  
- **Response Body:**
    ```json
    {
      "status": "Success",
      "data": [
        {
          "id": "d8e1c9d0-465f-4d1f-bb62-c3bcfa4b0f84",
          "name": "Programming",
          "createdDate": "2023-11-25T14:35:00",
          "updatedDate": "2023-11-25T14:35:00"
        },
        {
          "id": "a7d3e1c9-d950-44c1-aa84-c3ba5a7f9a32",
          "name": "Advanced Programming",
          "createdDate": "2023-11-20T12:15:00",
          "updatedDate": "2023-11-22T10:30:00"
        }
      ]
    }
    ```

**5. Kategori Detay Getir**  
- **Açıklama:** Belirtilen kategoriye ait detayları döner.  
- **HTTP Metodu:** `GET`  
- **URL:** `/Admin/category/{id}`  
- **Response Body:**
    ```json
    {
      "status": "Success",
      "data": {
        "id": "d8e1c9d0-465f-4d1f-bb62-c3bcfa4b0f84",
        "name": "Programming",
        "createdDate": "2023-11-25T14:35:00",
        "updatedDate": "2023-11-25T14:35:00"
      }
    }
    ```
   
</details>

<details>
  <summary>Kurs İşlemleri</summary>

**1. Yeni Kurs Oluştur**  
- **Açıklama:** Yeni bir kurs oluşturur.  
- **HTTP Metodu:** `POST`  
- **URL:** `/Admin/course`  
- **Request Body:**
    ```json
    {
      "name": "Introduction to C#",
      "description": "Learn the basics of C# programming.",
      "price": 49.99,
      "categoryId": "a1b2c3d4-5678-9101-1121-314151617181"
    }
    ```
- **Response Body:**
    ```json
    {
      "status": "Success",
      "data": "e1f2g3h4-1234-5678-9101-112131415161"
    }
    ```

**2. Kurs Güncelle**  
- **Açıklama:** Var olan bir kursu günceller.  
- **HTTP Metodu:** `PUT`  
- **URL:** `/Admin/course`  
- **Request Body:**
    ```json
    {
      "id": "e1f2g3h4-1234-5678-9101-112131415161",
      "name": "Advanced C#",
      "description": "Dive deep into C# programming concepts.",
      "price": 69.99,
      "categoryId": "a1b2c3d4-5678-9101-1121-314151617181"
    }
    ```
- **Response Body:**
    ```json
    {
      "status": "Success",
      "message": "Course updated successfully."
    }
    ```

  **3. Kurs Sil**  
- **Açıklama:** Belirtilen kursu siler.  
- **HTTP Metodu:** `DELETE`  
- **URL:** `/Admin/course/{id}`  
- **Response Body:**
    ```json
    {
      "status": "Success",
      "message": "Course deleted successfully."
    }
    ```

  **4. Tüm Kursları Getir**  
- **Açıklama:** Sistemdeki tüm kursları listeler.  
- **HTTP Metodu:** `GET`  
- **URL:** `/Admin/course`  
- **Response Body:**
    ```json
    {
      "status": "Success",
      "data": [
        {
          "id": "e1f2g3h4-1234-5678-9101-112131415161",
          "name": "Introduction to C#",
          "description": "Learn the basics of C# programming.",
          "price": 49.99,
          "categoryName": "Programming",
          "createdDate": "2023-11-25T14:35:00",
          "updatedDate": "2023-11-25T14:35:00"
        }
      ]
    }
    ```

  **5. Kurs Detay Getir**  
- **Açıklama:** Belirtilen kursa ait detayları döner.  
- **HTTP Metodu:** `GET`  
- **URL:** `/Admin/course/{id}`  
- **Response Body:**
    ```json
    {
      "status": "Success",
      "data": {
        "id": "e1f2g3h4-1234-5678-9101-112131415161",
        "name": "Introduction to C#",
        "description": "Learn the basics of C# programming.",
        "price": 49.99,
        "categoryName": "Programming",
        "createdDate": "2023-11-25T14:35:00",
        "updatedDate": "2023-11-25T14:35:00"
      }
    }
    ```

  **6. Belirli Bir Kategoriye Ait Kursları Getir**  
- **Açıklama:** Belirtilen kategoriye ait kursları listeler.  
- **HTTP Metodu:** `GET`  
- **URL:** `/GetCoursesByCategoryAsync/{categoryId}`  
- **Response Body:**
    ```json
    {
      "status": "Success",
      "data": [
        {
          "id": "e1f2g3h4-1234-5678-9101-112131415161",
          "name": "Introduction to C#",
          "description": "Learn the basics of C# programming.",
          "price": 49.99,
          "categoryName": "Programming",
          "createdDate": "2023-11-25T14:35:00",
          "updatedDate": "2023-11-25T14:35:00"
        }
      ]
    }
    ```

</details>
<details>
  <summary>User İşlemleri</summary>
  
  **1. Role Ekleme**  
 - **Açıklama:** Belirtilen kullanıcıya "admin" rolü ekler.  
 - **HTTP Metodu:** `POST`  
 - **URL:** `/Admin/AddRoleToUser/{UserId}`  
 - **Request Body:** (Burada body kullanılmaz, URL'den alınır)
 - **Response Body:**
    ```json
    {
        "status": "Success",
        "message": "Role added successfully."
    }
    ```
**2. Kullanıcıları Listele**  
- **Açıklama:** Tüm kullanıcıları ve her kullanıcının ilgili bilgilerini döner (Email, Kullanıcı adı, Cüzdan, Siparişler).  
- **HTTP Metodu:** `GET`  
- **URL:** `/Admin/AllUser`
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": [
          {
            "id": "user-id-123",
            "userName": "john_doe",
            "email": "john.doe@example.com",
            "wallet": 100.50,
            "orders": [
              {
                "id": "order-id-1"
              }
            ]
          }
        ]
    }
    ```

</details>
</details>
<br>
<details>
<summary><strong>Auth Controller</strong></summary>
  
**1. Kullanıcı Girişi** 
- **Açıklama:** Kullanıcı, sağladığı e-posta ve şifre ile sisteme giriş yapar. Eğer e-posta ve şifre doğruysa bir token döner.  
- **HTTP Metodu:** `POST`  
- **URL:** `/Auth/signin`  
- **Request Body:**
    ```json
      {
        "Email": "user@example.com",
        "Password": "userPassword123"
      }
      ```
- **Response Body:**
    ```json
      {
        "status": "Success",
        "data": {
          "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiZW1haWwiOiJqb2huLmRvZUBleGFtcGxlLmNvbSIsInRva2VuX2lkIjoiZjE2ZTczZDEtZDJhZC00ZThmLTkyYTItM2I2OTlhYmNiM2VhIiwibmFtZWRhdGEiOiJXYWx...
        }
      }
      ```

**2. Client Credential ile Giriş**

- **Açıklama:** Client, sağladığı `ClientId` ve `ClientSecret` ile giriş yapar. Giriş başarılıysa bir token döner.  
- **HTTP Metodu:** `POST`  
- **URL:** `/Auth/SignInClientCredential`  
- **Request Body:**
    ```json
      {
        "ClientId": "yourClientId",
        "ClientSecret": "yourClientSecret"
      }
      ```
- **Response Body:**
    ```json
      {
        "status": "Success",
        "data": {
          "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjbGllZW50SWQiOiJ5b3VyQ2xpZW50SWQiLCJ0b2tlbl9pZCI6ImZlZDczM2YtZDk5Yy00ZDJhLTg3YjYtYTgyYzYwOTNlZDhlIiwibmFtZWRhdGEiOiJXYWx...
        }
    }
      ```
</details>
</details>

<br>
<details>
<summary><strong>Order Controller</strong></summary>
<details>
  <summary>Order İşlemleri</summary>

**1. Sipariş Oluştur** 
- **Açıklama:** Kullanıcının sepetindeki ürünler üzerinden sipariş oluşturur ve toplam fiyat hesaplanır.
- **HTTP Metodu:** `POST`  
- **URL:** `/order`  
- **Request Body:**
    ```json
    {
        "UserId": "12345678-90ab-cdef-1234-567890abcdef",
        "BasketId": "abcdef12-3456-7890-abcd-ef1234567890"
      }
    ```
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": "abcdef12-3456-7890-abcd-ef1234567890"  // Sipariş ID'si
    }
    ```
**2. Sipariş Sil**
- **Açıklama:** Belirtilen ID'ye ait siparişi siler.  
- **HTTP Metodu:** `DELETE`  
- **URL:** `/order/{id}`  
- **Path Parametreleri:**
    - `id`: Silinecek siparişin ID'si.
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": null
    }
    ```

**3. Sipariş Detayı Getir**
- **Açıklama:** Belirtilen ID'ye ait siparişin detaylarını döner.  
- **HTTP Metodu:** `GET`  
- **URL:** `/order/{id}`  
- **Path Parametreleri:**
    - `id`: Getirilecek siparişin ID'si.
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": {
          "Id": "abcdef12-3456-7890-abcd-ef1234567890",
          "UserId": "12345678-90ab-cdef-1234-567890abcdef",
          "Wallet": 100.00,
          "BasketItemInCourseResponses": [
            {
              "Id": "courseId",
              "BasketId": "basketId",
              "BasketItemId": "basketItemId",
              "CategoryName": "Programming",
              "Name": "Introduction to C#",
              "CreatedDate": "2023-11-25T14:35:00",
              "Price": 49.99,
              "Quantity": 1,
              "Description": "Learn the basics of C# programming."
            }
          ],
          "CreatedDate": "2023-11-25T14:35:00",
          "UpdatedDate": "2023-11-25T14:35:00",
          "TotalAmount": 49.99,
          "Status": "Waiting"
        }
    }
     ```
**4. Tüm Siparişleri Listele**
- **Açıklama:** Sistemdeki tüm siparişlerin özet bilgilerini döner.  
- **HTTP Metodu:** `GET`  
- **URL:** `/order`  
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": [
          {
            "Id": "abcdef12-3456-7890-abcd-ef1234567890",
            "UserId": "12345678-90ab-cdef-1234-567890abcdef",
            "CreatedDate": "2023-11-25T14:35:00",
            "TotalAmount": 49.99,
            "Status": "Waiting"
          }
        ]
    }
    ```
**5. Kullanıcıya Ait Siparişleri Listele**
- **Açıklama:** Belirtilen kullanıcı ID'sine ait siparişleri döner.  
- **HTTP Metodu:** `GET`  
- **URL:** `/order/GetOrderByUser/{id}`  
- **Path Parametreleri:**
    - `id`: Kullanıcı ID'si.
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": [
          {
            "Id": "abcdef12-3456-7890-abcd-ef1234567890",
            "UserId": "12345678-90ab-cdef-1234-567890abcdef",
            "CreatedDate": "2023-11-25T14:35:00",
            "UpdatedDate": "2023-11-25T14:40:00",
            "TotalAmount": 49.99,
            "Status": "Waiting"
          }
        ]
    }
     ```
</details>

<details>
<summary>Sepet İşlemleri</summary>

**1. Sepete Kurs Ekle**
- **Açıklama:** Kullanıcıya ait sepete ürün ekler veya miktarını artırır.  
- **HTTP Metodu:** `POST`  
- **URL:** `/api/basket`  
- **Request Body:**
    ```json
    {
        "UserId": "12345678-90ab-cdef-1234-567890abcdef",
        "CourseId": "abcdef12-3456-7890-abcd-ef1234567890",
        "Quantity": 2
    }
    ```
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": "12345678-90ab-cdef-1234-567890abcdef" // Sepet ID'si
    }
    ```
**2. Sepetteki Kursu Sil**
- **Açıklama:** Kullanıcıya ait sepetten belirtilen kursu kaldırır.  
- **HTTP Metodu:** `DELETE`  
- **URL:** `/basket/DeleteCourseFromBasketAsync/{UserId}/{BasketItemId}`  
- **Path Parametreleri:**
    - `UserId`: Kullanıcının ID'si.
    - `BasketItemId`: Silinecek sepet öğesinin ID'si.
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": null
    }
    ```
**3. Kullanıcının Sepetini Listele**
- **Açıklama:** Kullanıcıya ait tüm sepet öğelerini döner.  
- **HTTP Metodu:** `GET`  
- **URL:** `/basket/{UserId}`  
- **Path Parametreleri:**
    - `UserId`: Kullanıcının ID'si.
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": [
          {
            "Id": "basketitem-1",
            "BasketId": "basket-1",
            "BasketItemId": "basketitem-1",
            "Name": "Course A",
            "Price": 100.00,
            "Description": "Description of Course A",
            "Quantity": 1,
            "CreatedDate": "2024-01-01T00:00:00Z",
            "UpdatedDate": "2024-01-01T00:00:00Z"
          }
        ]
    }
    ```
**4. Sepeti Sil**
- **Açıklama:** Belirtilen ID'ye ait sepeti tamamen siler.  
- **HTTP Metodu:** `DELETE`  
- **URL:** `/basket/{id}`  
- **Path Parametreleri:**
    - `id`: Silinecek sepetin ID'si.
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": null
    }
    ```
</details>
</details>

<br>
<details>
<summary><strong>User Controller</strong></summary>

**1. Kullanıcı Kayıt Ol**
- **Açıklama:** Yeni bir kullanıcı oluşturur.  
- **HTTP Metodu:** `POST`  
- **URL:** `/user`  
- **Request Body:**
    ```json
    {
        "UserName": "johndoe",
        "Email": "johndoe@example.com",
        "Password": "Password123!",
        "Wallet": 100.00
    }
    ```
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": "12345678-90ab-cdef-1234-567890abcdef"  // Kullanıcı ID'si
    }
    ```
**2. Kullanıcı Güncelle**
- **Açıklama:** Mevcut bir kullanıcının bilgilerini günceller.  
- **HTTP Metodu:** `PUT`  
- **URL:** `/user`  
- **Request Body:**
    ```json
    {
        "Id": "12345678-90ab-cdef-1234-567890abcdef",
        "UserName": "johnupdated",
        "Email": "johnupdated@example.com",
        "Wallet": 200.00
    }
    ```
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": null
    }
    ```

**3. Kullanıcı Sil**
- **Açıklama:** Belirtilen ID'ye ait kullanıcıyı siler.  
- **HTTP Metodu:** `DELETE`  
- **URL:** `/{id}`  
- **Path Parametreleri:**
    - `id`: Silinecek kullanıcının ID'si.
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": null
    }
    ```

**4. Kullanıcı Bilgilerini Getir**
- **Açıklama:** Belirtilen ID'ye ait kullanıcı bilgilerini döner.  
- **HTTP Metodu:** `GET`  
- **URL:** `/user/{id}`  
- **Path Parametreleri:**
    - `id`: Kullanıcının ID'si.
- **Response Body:**
    ```json
    {
        "status": "Success",
        "data": {
          "Id": "12345678-90ab-cdef-1234-567890abcdef",
          "UserName": "johndoe",
          "Email": "johndoe@example.com",
          "Wallet": 100.00,
          "Orders": [
            {
              "Id": "abcdef12-3456-7890-abcd-ef1234567890"  // Kullanıcının sipariş ID'leri
            }
          ]
        }
    }
    ```
</details>

<br>

<details>
<summary><strong>Payment Controller</strong></summary>

**1. Kullanıcı Kayıt Ol**
- **Açıklama:** Kullanıcı bir sipariş için ödeme işlemini gerçekleştirir. Kullanıcının bakiyesi sipariş tutarını karşılamıyorsa hata döner.  
- **HTTP Metodu:** `POST`  
- **URL:** `/payment`  
- **Request Body:**
    ```json
    {
        "userId": "12345678-90ab-cdef-1234-567890abcdef",
        "OrderId": "abcdef12-3456-7890-abcd-ef1234567890"
    }
    ```
- **Response Body (Başarılı):**
    ```json
    {
        "status": "Success",
        "data": {
          "Id": "payment-1",
          "Amount": 200.00,
          "OrderId": "order-1",
          "PaymentDate": "2024-01-01T00:00:00Z",
          "PaymentStatus": "Completed"
        }
    }
    ```
- **Response Body (Hata - Yetersiz Bakiye):**
    ```json
    {
        "status": "Fail",
        "message": "insufficient balance:50.00"
    }
    ```
</details>

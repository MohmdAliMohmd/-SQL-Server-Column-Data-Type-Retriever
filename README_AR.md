# 🗃️ مستخرج أنواع بيانات أعمدة SQL Server
> **Language Notice**: 
> [View in English (الانجليزية)](README.md) |

هذا التطبيق الكونسولي بلغة C# يستخرج معلومات مفصلة عن أنواع بيانات الأعمدة في جدول SQL Server باستخدام ADO.NET. يوفر واجهة سطر أوامر بسيطة للاتصال بنسخة SQL Server وفحص هيكل الجداول.

## 🚀 الميزات

- 🔍 استخراج دقيق لأنواع بيانات أعمدة SQL Server
- 🛠 معالجة أنواع البيانات الخاصة بمعاملات:
  - 📏 أنواع النصوص مع الطول (CHAR, VARCHAR, NCHAR, NVARCHAR)
  - 🔢 أنواع الأرقام العشرية مع الدقة والمقياس (DECIMAL, NUMERIC)
  - ⏱ أنواع الوقت مع الدقة (DATETIME2, TIME, DATETIMEOFFSET)
- 🔐 دعم كل من مصادقة Windows ومصادقة SQL Server
- 🔄 معالجة أسماء الأعمدة دون تمييز بين الحروف الكبيرة والصغيرة
- 🛡️ اتصال آمن مع التخلص الصحيح من الموارد
- 🚨 معالجة شاملة للأخطاء

## ⚙️ المتطلبات الأساسية

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download) أو أحدث
- نسخة SQL Server (2008 أو أحدث)
- 🔑 أذونات مناسبة للوصول إلى قواعد البيانات المستهدفة

## 🖥 كيفية الاستخدام

1. استنسخ المستودع:
   ```bash
   git clone https://github.com/MohmdAliMohmd/sql-column-type-retriever.git
   cd sql-column-type-retriever
   ```

2. بناء التطبيق:
   ```bash
   dotnet build
   ```

3. تشغيل التطبيق:
   ```bash
   dotnet run
   ```

4. اتبع التعليمات التفاعلية:
   ```bash
   مستخرج أنواع بيانات أعمدة SQL Server
   ======================================
   أدخل اسم خادم SQL: localhost\SQLEXPRESS
   أدخل اسم قاعدة البيانات: AdventureWorks
   أدخل طريقة المصادقة (1 لـ Windows، 2 لـ SQL Server): 1
   أدخل اسم الجدول: Person.Address
   ```

## 📊 مثال على الإخراج

```
أنواع بيانات الأعمدة:
------------------
AddressID: INT
AddressLine1: NVARCHAR(60)
AddressLine2: NVARCHAR(60)
City: NVARCHAR(30)
StateProvinceID: INT
PostalCode: NVARCHAR(15)
SpatialLocation: GEOMETRY
rowguid: UNIQUEIDENTIFIER
ModifiedDate: DATETIME
```

## 🧱 هيكل الكود

### المكونات الرئيسية:

1. **الطريقة الرئيسية**:
   - تتعامل مع إدخال المستخدم لتفاصيل الاتصال
   - تبني سلاسل اتصال آمنة
   - تستدعي طريقة استخراج الأنواع
   - تعرض النتائج

2. **طريقة الاستخراج الأساسية**:
   ```csharp
   static Dictionary<string, string> GetColumnDataTypes(
       string connectionString, 
       string tableName)
   {
       var columnInfo = new Dictionary<string, string>(
           StringComparer.OrdinalIgnoreCase);
       
       using (SqlConnection connection = new SqlConnection(connectionString))
       {
           connection.Open();
           DataTable schemaTable = connection.GetSchema("Columns", 
               new[] { null, null, tableName, null });
           
           foreach (DataRow row in schemaTable.Rows)
           {
               // معالجة وصف العمود
               // منطق معالجة الأنواع الخاصة
           }
       }
       return columnInfo;
   }
   ```

## 📋 أنواع البيانات المدعومة

| فئة نوع البيانات | أمثلة                  | التنسيق               |
|--------------------|---------------------------|----------------------|
| أنواع النصوص       | CHAR, VARCHAR, NCHAR      | TYPE(Length)         |
| أنواع MAX          | VARCHAR(MAX)              | TYPE(MAX)            |
| أنواع رقمية      | DECIMAL, NUMERIC          | TYPE(Precision,Scale)|
| أنواع زمنية     | DATETIME2, TIME           | TYPE(Precision)      |
| أنواع أخرى        | INT, UNIQUEIDENTIFIER     | TYPE                 |

## 🔒 ملاحظات الأمان

- 🔑 كلمات المرور لا يتم تخزينها أو عرضها
- 💾 سلاسل الاتصال توجد فقط في الذاكرة
- 🛡️ استخدام استخراج الهيكل بدلاً من SQL الديناميكي
- ♻️ التخلص الصحيح من الموارد باستخدام عبارات `using`
- 🚫 لا توجد تبعيات خارجية باستثناء .NET BCL

## 🤝 المساهمة

المساهمات مرحب بها! يرجى اتباع هذه الخطوات:

1. انسخ المستودع
2. أنشئ فرعًا جديدًا للميزة (`git checkout -b feature/اسم-التحسين`)
3. احفظ التغييرات (`git commit -am 'إضافة ميزة ما'`)
4. ادفع إلى الفرع (`git push origin feature/اسم-التحسين`)
5. أنشئ طلب سحب جديد

## 📜 الرخصة

هذا المشروع مرخص تحت رخصة MIT - راجع ملف [LICENSE](LICENSE) للتفاصيل.

---

**ملاحظة**: هذا التطبيق يستخرج فقط وصف هيكل البيانات ولا يصل أو يعدل بيانات الجدول. تأكد دائمًا من حصولك على الأذونات المناسبة قبل الوصول إلى هياكل قواعد البيانات.

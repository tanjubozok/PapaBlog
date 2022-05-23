namespace PapaBlog.Services.Utilities
{
    public static class Messages
    {
        public static class Generel
        {
            public static string TryCatch(string message)
            {
                return $"Try-Catch : {message}";
            }
        }

        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural)
                    return "Kategoriler bulunamadı.";
                return "Kategori bulunamdı.";
            }
            public static string IdNotFound()
            {
                return "Kategori-id bulunamadı.";
            }
            public static string Add(string categoryName)
            {
                return $"{categoryName} kategorisi başarılı bir şekilde eklendi.";
            }
            public static string NotAdding(string categoryName)
            {
                return $"{categoryName} kategorisi eklenemedi.";
            }
            public static string Delete(string categoryName)
            {
                return $"{categoryName} kategorisi başarılı bir şekilde silindi.";
            }
            public static string NotDeleting(string categoryName)
            {
                return $"{categoryName} kategorisi silinemedi.";
            }
            public static string HardDelete(string categoryName)
            {
                return $"{categoryName} kategorisi başarılı bir şekilde databaseden silindi.";
            }
            public static string NotHardDelete(string categoryName)
            {
                return $"{categoryName} kategorisi databaseden silinemedi.";
            }
            public static string Update(string categoryName)
            {
                return $"{categoryName} kategorisi başarılı bir şekilde güncellendi.";
            }
            public static string NotUpdating(string categoryName)
            {
                return $"{categoryName} kategorisi güncellenemedi.";
            }
        }

        public static class Article
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural)
                    return "Makaleler bulunamadı.";
                return "Makale bulunamdı.";
            }
            public static string IdNotFound()
            {
                return "Makale-id bulunamadı.";
            }
            public static string Add()
            {
                return "Makale başarılı bir şekilde eklendi.";
            }
            public static string NotAdding()
            {
                return "Makale eklenemedi.";
            }
            public static string Delete()
            {
                return "Makale başarılı bir şekilde silindi.";
            }
            public static string NotDeleting()
            {
                return "Makale silinemedi.";
            }
            public static string HardDelete()
            {
                return "Makale başarılı bir şekilde databaseden silindi.";
            }
            public static string NotHardDelete()
            {
                return "Makale databaseden silinemedi.";
            }
            public static string Update()
            {
                return "Makale başarılı bir şekilde güncellendi.";
            }
            public static string NotUpdating()
            {
                return "Makale güncellenemedi.";
            }
        }
    }
}

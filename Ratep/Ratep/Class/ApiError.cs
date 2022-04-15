using System.Windows;

namespace Ratep.Class
{
    public enum ApiErrors
    {
        ErrorNotFound = 0,
        DBNotFound = 404,
        UserNotFound = 405,
        ApiConnectionFailed = 406,
        PermissionDenied = 504,
        ErrorData = 505
    }

    public class ApiError
    {
        public static ApiErrors GetErrorByCode(int code)
        {
            switch (code)
            {
                case (int)ApiErrors.DBNotFound:
                    return ApiErrors.DBNotFound;
                case (int)ApiErrors.UserNotFound:
                    return ApiErrors.UserNotFound;
                case (int)ApiErrors.PermissionDenied:
                    return ApiErrors.PermissionDenied;
                case (int)ApiErrors.ErrorData:
                    return ApiErrors.ErrorData;
            }
            return ApiErrors.ErrorNotFound;
        }
        public static void ShowError(ApiErrors error)
        {
            switch (error)
            {
                case ApiErrors.ErrorNotFound:
                    MessageBox.Show("Произошла непредвиденная ошибка!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case ApiErrors.DBNotFound:
                    MessageBox.Show("Не удалось установить соединение с базой данных. Пожалуйста, повторите попытку позже.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case ApiErrors.UserNotFound:
                    MessageBox.Show("Указаный пользователь не найден.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case ApiErrors.PermissionDenied:
                    MessageBox.Show("Недостаточно прав, доступ запрещён!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case ApiErrors.ApiConnectionFailed:
                    MessageBox.Show("Не удалось установить соединение с сервером, пожалуйста повторите попытку позже.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case ApiErrors.ErrorData:
                    MessageBox.Show("Данные введены неверно!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
    }
}

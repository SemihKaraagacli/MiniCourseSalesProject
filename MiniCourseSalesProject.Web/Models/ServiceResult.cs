namespace MiniCourseSalesProject.Web.Models
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public string GetFirstError => Errors!.First();

        public bool IsError => Errors != null && Errors.Count > 0;
        public bool IsSuccess => !IsError;

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T>
            {
                Data = data,
            };
        }

        public static ServiceResult<T> Fail(List<string> Errors)
        {
            return new ServiceResult<T>
            {
                Errors = Errors,
            };
        }

        public static ServiceResult<T> Fail(string errors)
        {
            return new ServiceResult<T>
            {
                Errors = [errors]
            };
        }
    }


    public class ServiceResult
    {
        public List<string>? Errors { get; set; }

        public bool IsError => Errors != null && Errors.Count > 0;
        public bool IsSuccess => !IsError;
        public string GetFirstError => Errors!.First();

        public static ServiceResult Success()
        {
            return new ServiceResult();
        }

        public static ServiceResult Fail(List<string> Errors)
        {
            return new ServiceResult
            {
                Errors = Errors,
            };
        }

        public static ServiceResult Fail(string errors)
        {
            return new ServiceResult
            {
                Errors = [errors]
            };
        }
    }
}

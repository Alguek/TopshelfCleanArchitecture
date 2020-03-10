namespace TopshelfCleanArchitecture.Application.Interfaces
{
    public interface ILoggerFile
    {
        void Information(string messageTemplate);

        void Warning(string messageTemplate);

        void Debug<T>(string messageTemplate, T propertyValue);

        void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);

        public void Error<T>(string messageTemplate, T propertyValue);

        void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
        
        void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
        
        void Information<T>(string messageTemplate, T propertyValue);

        void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);

        void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);


    }
}

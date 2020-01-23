using System;

namespace Way2.Domain.Entities
{
    public class CityWeather : Entity<Guid>
    {

        #region Fields

        private City _city;
        private Guid _cityId;
        private DateTimeOffset _date;
        private float _temperature;

        #endregion

        #region Constructor

        public CityWeather(Guid cityId, DateTimeOffset date, float temperature)
        {
            this.CityId = cityId;
            this.Date = date;
            this.Temperature = temperature;
        }

        #endregion

        #region Properties

        public virtual City City
        {
            get => _city;
            set => SetWithNotify(value, ref _city);
        }

        public Guid CityId
        {
            get => _cityId;
            set => SetWithNotify(value, ref _cityId);
        }

        public DateTimeOffset Date
        {
            get => _date;
            set => SetWithNotify(value, ref _date);
        }

        public float Temperature
        {
            get => _temperature;
            set => SetWithNotify(value, ref _temperature);
        }

        #endregion

    }
}

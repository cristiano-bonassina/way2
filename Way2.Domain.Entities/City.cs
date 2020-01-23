using System;
using System.Collections.Generic;
using Way2.Common;

namespace Way2.Domain.Entities
{
    public class City : Entity<Guid>
    {

        #region Fields

        private string _name;

        #endregion

        #region Constructor

        public City(string name)
        {
            this.Name = name;
        }

        #endregion

        #region Properties

        public string Name
        {
            get => _name;
            set => SetWithNotify(value, ref _name);
        }

        public virtual ISet<CityWeather> WeatherHistory { get; } = new EntitySet<CityWeather>();

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerClass
{
    public class MonthlyConfigurationGestor
    {
        private Auxiliary AuxiliaryClass;
        public MonthlyConfigurationGestor()
        {
            this.AuxiliaryClass = new Auxiliary();
        }

        public DateTime CalculateMonthlyConfiguration(DateTime CurrentDay, FrecuencyType TheFrecuencyType,
            WeeklyValue TheWeeklyValue, int OccursValue, bool JumpTime, bool Repetition)
        {
            int Day = TheFrecuencyType != FrecuencyType.Last ? 1 : DateTime.DaysInMonth(CurrentDay.Year, CurrentDay.Month);
            int AddDay = TheFrecuencyType != FrecuencyType.Last ? 1 : -1;
            int TheDayOfWeek = this.GetDayOfWeek(TheWeeklyValue, TheFrecuencyType, CurrentDay);

            DateTime ResultDate = GetResultDate(TheWeeklyValue, this.AuxiliaryClass.ChangeDay(CurrentDay, Day), CurrentDay, AddDay, TheDayOfWeek, TheFrecuencyType, JumpTime, Repetition);

            if (ResultDate <= CurrentDay && Repetition == false)
            {
                DateTime NewCurrentDay = new DateTime(CurrentDay.Year, CurrentDay.Month, 1,
                    ResultDate.Hour, ResultDate.Minute, ResultDate.Second).AddMonths(OccursValue);
                ResultDate = this.CalculateMonthlyConfiguration(NewCurrentDay, TheFrecuencyType, TheWeeklyValue, OccursValue, JumpTime, true);
            }

            return ResultDate;
        }

        private DateTime GetResultDate(WeeklyValue TheWeeklyValue, DateTime FirstDayOfMonth, DateTime CurrentDay, 
            int AddDay, int TheDayOfWeek, FrecuencyType TheFrecuencyType, bool JumpTime, bool repeticion)
        {
            switch (TheWeeklyValue)
            {
                case WeeklyValue.Day:
                    FirstDayOfMonth = this.AuxiliaryClass.ChangeDay(CurrentDay, TheDayOfWeek);
                    break;
                case WeeklyValue.WeekDay:
                    FirstDayOfMonth = CalculateWeekDay(FirstDayOfMonth, CurrentDay, AddDay, TheFrecuencyType, JumpTime, repeticion);
                    break;
                case WeeklyValue.WeekendDay:
                    FirstDayOfMonth = CalculateWeekendDay(FirstDayOfMonth, CurrentDay, AddDay, TheFrecuencyType, JumpTime, repeticion);
                    break;
                default:
                    FirstDayOfMonth = CalculateWeeklyDate(FirstDayOfMonth, AddDay, TheFrecuencyType, (SchedulerWeek)TheDayOfWeek);
                    break;
            }
            return FirstDayOfMonth;
        }

        private DateTime CalculateWeekDay(DateTime FirstDayOfMonth, DateTime CurrentDay, int AddDay, FrecuencyType TheFrecuencyType, bool JumpTime, bool repeticion)
        {
            if (JumpTime)
            {
                FirstDayOfMonth = CurrentDay;
            }
            else
            {
                if (TheFrecuencyType == FrecuencyType.Last)
                {
                    while (this.AuxiliaryClass.GetSchedulerWeek(FirstDayOfMonth) != SchedulerWeek.Monday)
                    {
                        FirstDayOfMonth = FirstDayOfMonth.AddDays(AddDay);
                    }
                }
                else
                {
                    while (this.AuxiliaryClass.GetSchedulerWeek(FirstDayOfMonth) == SchedulerWeek.Saturday || 
                        this.AuxiliaryClass.GetSchedulerWeek(FirstDayOfMonth) == SchedulerWeek.Sunday)
                    {
                        FirstDayOfMonth = FirstDayOfMonth.AddDays(AddDay);
                    }
                }

                FirstDayOfMonth = CalculateFrecuency(TheFrecuencyType, FirstDayOfMonth, (int)this.AuxiliaryClass.GetSchedulerWeek(FirstDayOfMonth) - 1);

                if (FirstDayOfMonth <= CurrentDay && FirstDayOfMonth.AddDays(4) > CurrentDay && this.AuxiliaryClass.GetSchedulerWeek(CurrentDay) < SchedulerWeek.Friday && repeticion == false)
                {
                    FirstDayOfMonth = CurrentDay.AddDays(1);
                    if (FirstDayOfMonth.Month > CurrentDay.Month)
                    {
                        FirstDayOfMonth = FirstDayOfMonth.AddDays(-1);
                    }
                }
            }
            return FirstDayOfMonth;
        }

        private DateTime CalculateWeekendDay(DateTime FirstDayOfMonth, DateTime CurrentDay, int AddDay, FrecuencyType TheFrecuencyType, bool JumpTime, bool repeticion)
        {
            if (JumpTime)
            {
                FirstDayOfMonth = CurrentDay;
            }
            else
            {
                while (this.AuxiliaryClass.GetSchedulerWeek(FirstDayOfMonth) != SchedulerWeek.Saturday && 
                    this.AuxiliaryClass.GetSchedulerWeek(FirstDayOfMonth) != SchedulerWeek.Sunday)
                {
                    FirstDayOfMonth = FirstDayOfMonth.AddDays(AddDay);
                }

                FirstDayOfMonth = CalculateFrecuency(TheFrecuencyType, FirstDayOfMonth);

                if (this.AuxiliaryClass.GetSchedulerWeek(FirstDayOfMonth) == SchedulerWeek.Sunday && FirstDayOfMonth.Day != 1)
                {
                    FirstDayOfMonth = FirstDayOfMonth.AddDays(-1);
                }

                if (FirstDayOfMonth == CurrentDay && this.AuxiliaryClass.GetSchedulerWeek(FirstDayOfMonth) == SchedulerWeek.Saturday && repeticion == false)
                {
                    FirstDayOfMonth = FirstDayOfMonth.AddDays(1);
                    if (FirstDayOfMonth.Month > CurrentDay.Month)
                    {
                        FirstDayOfMonth = FirstDayOfMonth.AddDays(-1);
                    }
                }
            }
            return FirstDayOfMonth;
        }

        private DateTime CalculateWeeklyDate(DateTime FirstDayOfMonth, int AddDay, FrecuencyType TheFrecuencyType, SchedulerWeek TheDayOfWeek)
        {
            while (this.AuxiliaryClass.GetSchedulerWeek(FirstDayOfMonth) != TheDayOfWeek)
            {
                FirstDayOfMonth = FirstDayOfMonth.AddDays(AddDay);
            }
            return CalculateFrecuency(TheFrecuencyType, FirstDayOfMonth);
        }

        private DateTime CalculateFrecuency(FrecuencyType TheFrecuencyType, DateTime FirstDayOfMonth, int SchedulerWeek = 0)
        {
            switch (TheFrecuencyType)
            {
                case FrecuencyType.Second:
                    FirstDayOfMonth = FirstDayOfMonth.AddDays(7 - SchedulerWeek);
                    break;
                case FrecuencyType.Third:
                    FirstDayOfMonth = FirstDayOfMonth.AddDays(14 - SchedulerWeek);
                    break;
                case FrecuencyType.Fourth:
                    FirstDayOfMonth = FirstDayOfMonth.AddDays(21 - SchedulerWeek);
                    break;
            }
            return FirstDayOfMonth;
        }

        private int GetDayOfWeek(WeeklyValue TheWeeklyValue, FrecuencyType TheFrecuencyType, DateTime TheCurrentDay)
        {
            int Result = 0;
            switch (TheWeeklyValue)
            {
                case WeeklyValue.Monday:
                case WeeklyValue.Tuesday:
                case WeeklyValue.Wednesday:
                case WeeklyValue.Thursday:
                case WeeklyValue.Friday:
                case WeeklyValue.Saturday:
                case WeeklyValue.Sunday:
                    Result = (int)TheWeeklyValue;
                    break;
                case WeeklyValue.Day:
                    switch (TheFrecuencyType)
                    {
                        case FrecuencyType.First:
                        case FrecuencyType.Second:
                        case FrecuencyType.Third:
                        case FrecuencyType.Fourth:
                            Result = (int)TheFrecuencyType;
                            break;
                        case FrecuencyType.Last:
                            Result = DateTime.DaysInMonth(TheCurrentDay.Year, TheCurrentDay.Month);
                            break;
                    }
                    break;
            }
            return Result;
        }
    }
}
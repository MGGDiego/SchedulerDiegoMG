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
            WeeklyValue TheWeeklyValue, int OccursValue, bool JumpTime, bool repeticion)
        {
            int Day = 1;
            int AddDay = 1;

            if (TheFrecuencyType == FrecuencyType.Last && TheWeeklyValue != WeeklyValue.WeekDay)
            {
                Day = DateTime.DaysInMonth(CurrentDay.Year, CurrentDay.Month);
                AddDay = -1;
            }

            int TheDayOfWeek = this.GetDayOfWeek(TheWeeklyValue, TheFrecuencyType, CurrentDay);

            DateTime FirstDayOfMonth;
            if ((int)TheWeeklyValue == 8)
            {
                FirstDayOfMonth = this.AuxiliaryClass.ChangeDay(CurrentDay, TheDayOfWeek);
            }
            else if ((int)TheWeeklyValue == 9)
            {
                if (JumpTime)
                {
                    FirstDayOfMonth = CurrentDay;
                }
                else
                {
                    FirstDayOfMonth = this.AuxiliaryClass.ChangeDay(CurrentDay, Day);
                    int FirstDayOfMonthValue = this.AuxiliaryClass.GetSchedulerWeek(FirstDayOfMonth);
                    while (FirstDayOfMonthValue == 6 || FirstDayOfMonthValue == 7)
                    {
                        FirstDayOfMonth = FirstDayOfMonth.AddDays(AddDay);
                        FirstDayOfMonthValue = GetMonthlyDay(FirstDayOfMonthValue, AddDay);
                    }

                    FirstDayOfMonth = CalculateFrecuency(TheFrecuencyType, FirstDayOfMonth);

                    if (FirstDayOfMonthValue != 1 && TheFrecuencyType != FrecuencyType.First)
                    {
                        FirstDayOfMonthValue = 6;
                        FirstDayOfMonth = FirstDayOfMonth.AddDays(-1);
                    }

                    if (FirstDayOfMonth == CurrentDay && FirstDayOfMonthValue == 6 && repeticion == false)
                    {
                        FirstDayOfMonth = FirstDayOfMonth.AddDays(1);
                        if (FirstDayOfMonth.Month > CurrentDay.Month)
                        {
                            FirstDayOfMonth = FirstDayOfMonth.AddDays(-1);
                        }
                    }
                }
            }
            else if ((int)TheWeeklyValue == 10)
            {
                if (JumpTime)
                {
                    FirstDayOfMonth = CurrentDay;
                }
                else
                {
                    FirstDayOfMonth = this.AuxiliaryClass.ChangeDay(CurrentDay, Day);
                    int FirstDayOfMonthValue = this.AuxiliaryClass.GetSchedulerWeek(FirstDayOfMonth);
                    while (FirstDayOfMonthValue != 6 && FirstDayOfMonthValue != 7)
                    {
                        FirstDayOfMonth = FirstDayOfMonth.AddDays(AddDay);
                        FirstDayOfMonthValue = GetMonthlyDay(FirstDayOfMonthValue, AddDay);
                    }

                    FirstDayOfMonth = CalculateFrecuency(TheFrecuencyType, FirstDayOfMonth);

                    if (FirstDayOfMonthValue == 7 && TheFrecuencyType != FrecuencyType.First)
                    {
                        FirstDayOfMonthValue = 6;
                        FirstDayOfMonth = FirstDayOfMonth.AddDays(-1);
                    }

                    if (FirstDayOfMonth == CurrentDay && FirstDayOfMonthValue == 6 && repeticion == false)
                    {
                        FirstDayOfMonth = FirstDayOfMonth.AddDays(1);
                        if (FirstDayOfMonth.Month > CurrentDay.Month)
                        {
                            FirstDayOfMonth = FirstDayOfMonth.AddDays(-1);
                        }
                    }
                }
            }
            else
            {
                FirstDayOfMonth = this.AuxiliaryClass.ChangeDay(CurrentDay, Day);
                int FirstDayOfMonthValue = this.AuxiliaryClass.GetSchedulerWeek(FirstDayOfMonth);
                while (FirstDayOfMonthValue != TheDayOfWeek)
                {
                    FirstDayOfMonth = FirstDayOfMonth.AddDays(AddDay);
                    FirstDayOfMonthValue = GetMonthlyDay(FirstDayOfMonthValue, AddDay);
                }

                FirstDayOfMonth = CalculateFrecuency(TheFrecuencyType, FirstDayOfMonth);
            }

            if (FirstDayOfMonth <= CurrentDay && repeticion == false)
            {
                DateTime NewCurrentDay = new DateTime(CurrentDay.Year, CurrentDay.Month, 1,
                    FirstDayOfMonth.Hour, FirstDayOfMonth.Minute, FirstDayOfMonth.Second).AddMonths(OccursValue);
                FirstDayOfMonth = this.CalculateMonthlyConfiguration(NewCurrentDay, TheFrecuencyType, TheWeeklyValue, OccursValue, JumpTime, true);
            }

            return FirstDayOfMonth;
        }

        private int GetMonthlyDay(int FirstDayOfMonthValue, int AddDay)
        {
            if (AddDay == 1)
            {
                if (FirstDayOfMonthValue >= 7) FirstDayOfMonthValue = 0;
                FirstDayOfMonthValue++;
            }
            else
            {
                if (FirstDayOfMonthValue <= 1) FirstDayOfMonthValue = 8;
                FirstDayOfMonthValue--;
            }
            return FirstDayOfMonthValue;
        }

        private DateTime CalculateFrecuency(FrecuencyType TheFrecuencyType, DateTime FirstDayOfMonth)
        {
            switch (TheFrecuencyType)
            {
                case FrecuencyType.Second:
                    FirstDayOfMonth = FirstDayOfMonth.AddDays(7);
                    break;
                case FrecuencyType.Third:
                    FirstDayOfMonth = FirstDayOfMonth.AddDays(14);
                    break;
                case FrecuencyType.Fourth:
                    FirstDayOfMonth = FirstDayOfMonth.AddDays(21);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JwrightBLL.BLL
{

    #region jwright enum
    public enum School_Status
    {
        Active = 1,
        Inactive = 2,
        Resigned = 3
    }

    public enum Person_Gender
    {
        Male = 1,
        Female = 2
    }

    public enum Marital_Status
    {
        Single = 1,
        Married = 2,
        Separated = 3
    }

    public enum Term
    {
        Regular = 1,
        Per_Session = 2
    }

    public enum School_Role
    {
        Admin = 0,
        Student = 1,
        Instructor = 2,
        Staff = 3,
        Guest = 4
    }

    #endregion


    public enum MatchingBonusType
    {
        None = 0,
        LifetimePPPS = 1,
        LifetimePPS = 2,
        AnnualPPPS = 3,
        AnnualPPS = 4
    }
    public enum Point_Type
    {
        Personal = 0,
        Group = 1
    }
    public enum PlacementPosition
    {
        Left = 0,
        Right = 1
    }
}
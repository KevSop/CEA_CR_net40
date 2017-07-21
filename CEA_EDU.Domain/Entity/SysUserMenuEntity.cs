using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Domain.Entity
{
    [Table("SysUserMenu")]
    public class SysUserMenuEntity
    {
        #region Constructor
        public SysUserMenuEntity() { }

        public SysUserMenuEntity(String userCode,String menuId,String menuRights)
        {
            this.userCode = userCode;
            this.menuId = menuId;
            this.menuRights = menuRights;
        }
        #endregion

        #region Attributes
        private String userCode;

        public String UserCode
        {
            get { return this.userCode; }
            set { this.userCode = value; }
        }
        private String menuId;

        public String MenuId
        {
            get { return this.menuId; }
            set { this.menuId = value; }
        }
        private String menuRights;

        public String MenuRights
        {
            get { return this.menuRights; }
            set { this.menuRights = value; }
        }
        #endregion

        #region Validator
        public List<string> ErrorList = new List<string>();
        private bool Validator()
        {    
            bool validatorResult = true;
            if (string.IsNullOrEmpty(this.UserCode))
            {
                validatorResult = false;
                this.ErrorList.Add("The UserCode should not be empty!");
            }
            if (this.UserCode != null && 20 < this.UserCode.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of UserCode should not be greater then 20!");
            }
            if (string.IsNullOrEmpty(this.MenuId))
            {
                validatorResult = false;
                this.ErrorList.Add("The MenuId should not be empty!");
            }
            if (this.MenuId != null && 36 < this.MenuId.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of MenuId should not be greater then 36!");
            }
            if (string.IsNullOrEmpty(this.MenuRights))
            {
                validatorResult = false;
                this.ErrorList.Add("The MenuRights should not be empty!");
            }
            if (this.MenuRights != null && 1 < this.MenuRights.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of MenuRights should not be greater then 1!");
            }
            return validatorResult;
        }    
        #endregion
    }
}

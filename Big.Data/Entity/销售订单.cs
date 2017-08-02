﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Big.Data.Entity
{
    /// <summary>销售订单</summary>
    [Serializable]
    [DataObject]
    [Description("销售订单")]
    [BindIndex("IU_SalesOrder_Number", true, "Number")]
    [BindTable("SalesOrder", Description = "销售订单", ConnName = "Data", DbType = DatabaseType.SqlServer)]
    public partial class SalesOrder : ISalesOrder
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 10)]
        [BindColumn("ID", "编号", "int", 10, 0)]
        public virtual Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private String _Number;
        /// <summary>订单号</summary>
        [DisplayName("订单号")]
        [Description("订单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Number", "订单号", "nvarchar(50)", 0, 0)]
        public virtual String Number
        {
            get { return _Number; }
            set { if (OnPropertyChanging(__.Number, value)) { _Number = value; OnPropertyChanged(__.Number); } }
        }

        private Int32 _NodeID;
        /// <summary>节点</summary>
        [DisplayName("节点")]
        [Description("节点")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn("NodeID", "节点", "int", 10, 0)]
        public virtual Int32 NodeID
        {
            get { return _NodeID; }
            set { if (OnPropertyChanging(__.NodeID, value)) { _NodeID = value; OnPropertyChanged(__.NodeID); } }
        }

        private String _Action;
        /// <summary>动作</summary>
        [DisplayName("动作")]
        [Description("动作")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Action", "动作", "nvarchar(50)", 0, 0)]
        public virtual String Action
        {
            get { return _Action; }
            set { if (OnPropertyChanging(__.Action, value)) { _Action = value; OnPropertyChanged(__.Action); } }
        }

        private DateTime _CreateTime;
        /// <summary>时间</summary>
        [DisplayName("时间")]
        [Description("时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn("CreateTime", "时间", "datetime", 3, 0)]
        public virtual DateTime CreateTime
        {
            get { return _CreateTime; }
            set { if (OnPropertyChanging(__.CreateTime, value)) { _CreateTime = value; OnPropertyChanged(__.CreateTime); } }
        }
        #endregion

        #region 获取/设置 字段值
        /// <summary>
        /// 获取/设置 字段值。
        /// 一个索引，基类使用反射实现。
        /// 派生实体类可重写该索引，以避免反射带来的性能损耗
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.ID : return _ID;
                    case __.Number : return _Number;
                    case __.NodeID : return _NodeID;
                    case __.Action : return _Action;
                    case __.CreateTime : return _CreateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Number : _Number = Convert.ToString(value); break;
                    case __.NodeID : _NodeID = Convert.ToInt32(value); break;
                    case __.Action : _Action = Convert.ToString(value); break;
                    case __.CreateTime : _CreateTime = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得销售订单字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>订单号</summary>
            public static readonly Field Number = FindByName(__.Number);

            ///<summary>节点</summary>
            public static readonly Field NodeID = FindByName(__.NodeID);

            ///<summary>动作</summary>
            public static readonly Field Action = FindByName(__.Action);

            ///<summary>时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得销售订单字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>订单号</summary>
            public const String Number = "Number";

            ///<summary>节点</summary>
            public const String NodeID = "NodeID";

            ///<summary>动作</summary>
            public const String Action = "Action";

            ///<summary>时间</summary>
            public const String CreateTime = "CreateTime";

        }
        #endregion
    }

    /// <summary>销售订单接口</summary>
    public partial interface ISalesOrder
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>订单号</summary>
        String Number { get; set; }

        /// <summary>节点</summary>
        Int32 NodeID { get; set; }

        /// <summary>动作</summary>
        String Action { get; set; }

        /// <summary>时间</summary>
        DateTime CreateTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Big.Data
{
    /// <summary>销售订单</summary>
    [Serializable]
    [DataObject]
    [Description("销售订单")]
    [BindIndex("IU_SalesOrder_Number", true, "Number")]
    [BindTable("SalesOrder", Description = "销售订单", ConnName = "Data", DbType = DatabaseType.SqlServer)]
    public partial class SalesOrder
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get => _ID; set { if (OnPropertyChanging("ID", value)) { _ID = value; OnPropertyChanged("ID"); } } }

        private String _Number;
        /// <summary>订单号</summary>
        [DisplayName("订单号")]
        [Description("订单号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Number", "订单号", "")]
        public String Number { get => _Number; set { if (OnPropertyChanging("Number", value)) { _Number = value; OnPropertyChanged("Number"); } } }

        private Int32 _NodeID;
        /// <summary>节点</summary>
        [DisplayName("节点")]
        [Description("节点")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("NodeID", "节点", "")]
        public Int32 NodeID { get => _NodeID; set { if (OnPropertyChanging("NodeID", value)) { _NodeID = value; OnPropertyChanged("NodeID"); } } }

        private String _Action;
        /// <summary>动作</summary>
        [DisplayName("动作")]
        [Description("动作")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Action", "动作", "")]
        public String Action { get => _Action; set { if (OnPropertyChanging("Action", value)) { _Action = value; OnPropertyChanged("Action"); } } }

        private DateTime _CreateTime;
        /// <summary>时间</summary>
        [DisplayName("时间")]
        [Description("时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("CreateTime", "时间", "")]
        public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case "ID": return _ID;
                    case "Number": return _Number;
                    case "NodeID": return _NodeID;
                    case "Action": return _Action;
                    case "CreateTime": return _CreateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "ID": _ID = value.ToInt(); break;
                    case "Number": _Number = Convert.ToString(value); break;
                    case "NodeID": _NodeID = value.ToInt(); break;
                    case "Action": _Action = Convert.ToString(value); break;
                    case "CreateTime": _CreateTime = value.ToDateTime(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得销售订单字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName("ID");

            /// <summary>订单号</summary>
            public static readonly Field Number = FindByName("Number");

            /// <summary>节点</summary>
            public static readonly Field NodeID = FindByName("NodeID");

            /// <summary>动作</summary>
            public static readonly Field Action = FindByName("Action");

            /// <summary>时间</summary>
            public static readonly Field CreateTime = FindByName("CreateTime");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得销售订单字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>订单号</summary>
            public const String Number = "Number";

            /// <summary>节点</summary>
            public const String NodeID = "NodeID";

            /// <summary>动作</summary>
            public const String Action = "Action";

            /// <summary>时间</summary>
            public const String CreateTime = "CreateTime";
        }
        #endregion
    }
}
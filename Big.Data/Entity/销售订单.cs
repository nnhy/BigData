using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Big.Data;

/// <summary>销售订单</summary>
[Serializable]
[DataObject]
[Description("销售订单")]
[BindIndex("IU_SalesOrder_Number", true, "Number")]
[BindTable("SalesOrder", Description = "销售订单", ConnName = "Data", DbType = DatabaseType.SqlServer)]
public partial class SalesOrder
{
    #region 属性
    private Int64 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int64 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _Number;
    /// <summary>订单号</summary>
    [DisplayName("订单号")]
    [Description("订单号")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Number", "订单号", "")]
    public String Number { get => _Number; set { if (OnPropertyChanging("Number", value)) { _Number = value; OnPropertyChanged("Number"); } } }

    private Int32 _NodeId;
    /// <summary>节点</summary>
    [DisplayName("节点")]
    [Description("节点")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("NodeId", "节点", "")]
    public Int32 NodeId { get => _NodeId; set { if (OnPropertyChanging("NodeId", value)) { _NodeId = value; OnPropertyChanged("NodeId"); } } }

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
        get => name switch
        {
            "Id" => _Id,
            "Number" => _Number,
            "NodeId" => _NodeId,
            "Action" => _Action,
            "CreateTime" => _CreateTime,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToLong(); break;
                case "Number": _Number = Convert.ToString(value); break;
                case "NodeId": _NodeId = value.ToInt(); break;
                case "Action": _Action = Convert.ToString(value); break;
                case "CreateTime": _CreateTime = value.ToDateTime(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static SalesOrder FindById(Int64 id)
    {
        if (id < 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }
    #endregion

    #region 字段名
    /// <summary>取得销售订单字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>订单号</summary>
        public static readonly Field Number = FindByName("Number");

        /// <summary>节点</summary>
        public static readonly Field NodeId = FindByName("NodeId");

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
        public const String Id = "Id";

        /// <summary>订单号</summary>
        public const String Number = "Number";

        /// <summary>节点</summary>
        public const String NodeId = "NodeId";

        /// <summary>动作</summary>
        public const String Action = "Action";

        /// <summary>时间</summary>
        public const String CreateTime = "CreateTime";
    }
    #endregion
}

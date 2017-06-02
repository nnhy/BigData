/*
 * XCoder v6.9.6345.18728
 * 作者：nnhy/X2
 * 时间：2017-05-16 10:48:58
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using NewLife.Log;
using NewLife.Web;
using NewLife.Data;
using XCode;
using XCode.Configuration;
using XCode.Membership;

namespace Big.Data.Entity
{
    /// <summary>销售订单</summary>
    public partial class SalesOrder : Entity<SalesOrder>
    {
        #region 对象操作
        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            //base.Valid(isNew);
        }
        #endregion

        #region 扩展属性


        #endregion

        #region 扩展查询

        /// <summary>根据订单号查找</summary>
        /// <param name="number">订单号</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static SalesOrder FindByNumber(String number)
        {
            if (Meta.Count >= 1000)
                return Find(__.Number, number);
            else // 实体缓存
                return Meta.Cache.Entities.Find(__.Number, number);
            // 单对象缓存
            //return Meta.SingleCache[number];
        }

        #endregion

        #region 高级查询
        /// <summary>查询满足条件的记录集，分页、排序</summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="key">关键字</param>
        /// <param name="param">分页排序参数，同时返回满足条件的总记录数</param>
        /// <returns>实体集</returns>
        public static EntityList<SalesOrder> Search(DateTime start, DateTime end, String key, PageParameter param)
        {
            var exp = new WhereExpression();

            if (!key.IsNullOrEmpty()) exp &= _.Number == key.Trim();
            exp &= _.CreateTime.Between(start, end);

            return FindAll(exp, param);
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion
    }
}
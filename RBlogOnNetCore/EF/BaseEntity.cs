using System;
namespace RBlogOnNetCore.EF
{
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// 自加型id
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// 实现等式算法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj as BaseEntity);
        }
        private static bool IsTransient(BaseEntity obj)
        {
            return obj != null && Equals(obj.Id, default(int));
        }
        /// <summary>
        /// 获取非代理类型
        /// </summary>
        /// <returns></returns>
        private Type GetUnproxiedType()
        {
            return GetType();
        }
        public virtual bool Equals(BaseEntity other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }

            return false;
        }
        /// <summary>
        /// 用于支持以后的Dictionary和HashTable数据集调用
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (Equals(Id, default(int)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }
        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            return Equals(x, y);
        }
        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !(x == y);
        }
    }
}

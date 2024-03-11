using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using JetBrains.Annotations;
using System.Security.Principal;

namespace CmsKitDemo.Entities
{
    public class Box : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual required string Section { get;  set; }
        public virtual string? Title { get; protected set; }
        public virtual string? Action { get; protected set; }
        public virtual string? ActionUrl { get; protected set; }
        public virtual string? Summary { get; protected set; }
        public virtual  BoxStatus Status { get; protected set; }
        public virtual string? Description { get; protected set; }
        public virtual ICollection<BoxItem> BoxItems { get; protected set; }

        protected Box()
        {
           
        }
        protected internal Box(Guid id, [NotNull] string section, string? title = null, string? action = null, string? actionUrl = null, string? summary = null, BoxStatus status = BoxStatus.Draft, string? description = null) : base(id)
        {
            Check.NotNullOrWhiteSpace(section, nameof(section), maxLength: BoxConsts.SectionMaxLength);
            Check.Length(title, nameof(title), BoxConsts.TitleMaxLength);
            Check.Length(action, nameof(action), BoxConsts.ActionMaxLength);
            Check.Length(actionUrl, nameof(actionUrl), BoxConsts.ActionUrlMaxLength);
            Check.Length(summary, nameof(summary), BoxConsts.SummaryMaxLength);
            Check.Length(description, nameof(description), BoxConsts.DescriptionMaxLength);
            Id = id;
            BoxItems = new List<BoxItem>();
            Section = section;
            Title = title;
            Action = action;
            ActionUrl = actionUrl;
            Summary = summary;
            Status = status;
            Description = description;
        }

        //public Box(Guid id, Guid coverImageMediaId, [NotNull] string description) : base(id)
        //{
        //    Description = Check.NotNullOrWhiteSpace(description, nameof(description), maxLength: CmsKitDemoConsts.MaxDescriptionLength);
        //}
    }
}

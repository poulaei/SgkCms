using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Volo.Abp;

namespace  CmsKitDemo.Entities;

[Serializable]
public class BoxSectionAlreadyExistsException : BusinessException
{
    public BoxSectionAlreadyExistsException([NotNull] string section)
    {
        Code = ErrorCodes.Boxes.SectionAlreadyExist;
        WithData(nameof(Box.Section), section);
    }

    public BoxSectionAlreadyExistsException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {

    }
}

import type { CreationAuditedEntityDto, EntityDto } from '@abp/ng.core';

export interface CreateUpdateGalleryImageDto {
  description?: string;
  coverImageMediaId?: string;
}

export interface GalleryImageDto extends CreationAuditedEntityDto<string> {
  description?: string;
  coverImageMediaId?: string;
}

export interface GalleryImageWithDetailsDto extends EntityDto<string> {
  description?: string;
  coverImageMediaId?: string;
  likeCount: number;
  commentCount: number;
}

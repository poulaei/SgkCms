import type { ExtensibleEntityDto, ExtensibleObject } from '@abp/ng.core';
import type { BoxStatus } from '../../entities/box-status.enum';

export interface BoxDto extends ExtensibleEntityDto<string> {
  section?: string;
  title?: string;
  action?: string;
  actionUrl?: string;
  summary?: string;
  status: BoxStatus;
  description?: string;
  concurrencyStamp?: string;
  boxItems: BoxItemDto[];
}

export interface BoxItemDto extends ExtensibleEntityDto<string> {
  boxId?: string;
  title?: string;
  action?: string;
  actionUrl?: string;
  summary?: string;
  icon?: string;
  description?: string;
  concurrencyStamp?: string;
  mediaId?: string;
}

export interface CreateBoxItemDto extends ExtensibleObject {
  boxId: string;
  title?: string;
  action?: string;
  actionUrl?: string;
  summary?: string;
  description?: string;
  mediaId?: string;
}

export interface UpdateBoxItemDto extends ExtensibleObject {
  title?: string;
  action?: string;
  actionUrl?: string;
  summary?: string;
  description?: string;
  mediaId?: string;
  concurrencyStamp?: string;
}

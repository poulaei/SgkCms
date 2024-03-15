import { mapEnumToOptions } from '@abp/ng.core';

export enum BoxStatus {
  Draft = 0,
  Published = 1,
  Expired = 2,
}

export const boxStatusOptions = mapEnumToOptions(BoxStatus);

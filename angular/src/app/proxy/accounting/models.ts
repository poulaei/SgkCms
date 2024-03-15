import type { LedgerGroupType } from './ledger-groups/ledger-group-type.enum';
import type { AuditedEntityDto } from '@abp/ng.core';

export interface LedgerGroupCreateUpdateDto {
  code: number;
  title: string;
  type: LedgerGroupType;
  description?: string;
}

export interface LedgerGroupDto extends AuditedEntityDto<string> {
  code: number;
  title?: string;
  type: LedgerGroupType;
  description?: string;
}

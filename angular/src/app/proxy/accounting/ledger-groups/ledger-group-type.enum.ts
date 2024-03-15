import { mapEnumToOptions } from '@abp/ng.core';

export enum LedgerGroupType {
  BalanceSheet = 1,
  ProfitAndLoss = 2,
  Disciplinary = 3,
}

export const ledgerGroupTypeOptions = mapEnumToOptions(LedgerGroupType);

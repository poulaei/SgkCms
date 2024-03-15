import type { LedgerGroupCreateUpdateDto, LedgerGroupDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LedgerGroupService {
  apiName = 'Default';
  

  create = (input: LedgerGroupCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LedgerGroupDto>({
      method: 'POST',
      url: '/api/app/ledger-group',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/ledger-group/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LedgerGroupDto>({
      method: 'GET',
      url: `/api/app/ledger-group/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LedgerGroupDto>>({
      method: 'GET',
      url: '/api/app/ledger-group',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: LedgerGroupCreateUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LedgerGroupDto>({
      method: 'PUT',
      url: `/api/app/ledger-group/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}

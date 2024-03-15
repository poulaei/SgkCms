import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { LedgerGroupDto, LedgerGroupService } from '@proxy/accounting'
import { ledgerGroupTypeOptions } from '../proxy/accounting/ledger-groups';

@Component({
  selector: 'app-ledgergroup',
  templateUrl: './ledgergroup.component.html',
  styleUrls: ['./ledgergroup.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
})
export class LedgergroupComponent implements OnInit {
  ledgerGroup = { items: [], totalCount: 0 } as PagedResultDto<LedgerGroupDto>;
  form: FormGroup;
  selectedLedgergroup = {} as LedgerGroupDto;
  ledgergroupTypes = ledgerGroupTypeOptions;

  isModalOpen = false;

  constructor(
    public readonly list: ListService,
    private ledgerGroupService: LedgerGroupService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) {
  }
  ngOnInit() {
    const ledgerGroupStreamCreator = (query) => this.ledgerGroupService.getList(query);

    this.list.hookToQuery(ledgerGroupStreamCreator).subscribe((response) => {
      this.ledgerGroup = response;
    }); 
  }
  createLedgerGroup() {
    this.selectedLedgergroup = {} as LedgerGroupDto;
    this.buildForm();
    this.isModalOpen = true;
  }
  editLedgerGroup(id: string) {
    this.ledgerGroupService.get(id).subscribe((ledgerGroup) => {
      this.selectedLedgergroup = ledgerGroup;
      this.buildForm();
      this.isModalOpen = true;
    });
  }
  deleteLedgerGroup(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.ledgerGroupService.delete(id).subscribe(() => this.list.get());
      }
    });
  }
  buildForm() {
    this.form = this.fb.group({
      //authorId: [this.selectedLedgergroup. || null, Validators.required],
      title: [this.selectedLedgergroup.title || null, Validators.required],
      code: [this.selectedLedgergroup.code || null, Validators.required],
      type: [this.selectedLedgergroup.type || null, Validators.required],
      description: [this.selectedLedgergroup.description || null, Validators.maxLength(256)],
    });
  }
  save() {
    if (this.form.invalid) {
      return;
    }
    const request = this.selectedLedgergroup.id
      ? this.ledgerGroupService.update(this.selectedLedgergroup.id, this.form.value)
      : this.ledgerGroupService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}

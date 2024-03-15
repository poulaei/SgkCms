import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LedgergroupComponent } from './ledgergroup.component';

describe('LedgergroupComponent', () => {
  let component: LedgergroupComponent;
  let fixture: ComponentFixture<LedgergroupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LedgergroupComponent]
    });
    fixture = TestBed.createComponent(LedgergroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

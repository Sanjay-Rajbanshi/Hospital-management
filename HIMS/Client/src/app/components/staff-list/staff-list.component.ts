import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StaffService } from '../../services/staff.service';
import { ReactiveFormsModule, FormBuilder, FormGroup,Validator } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AgGridModule } from 'ag-grid-angular';
import { ModuleRegistry, AllCommunityModule } from 'ag-grid-community';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-staff-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,HttpClientModule,AgGridModule],
  templateUrl: './staff-list.component.html',
  styleUrl: './staff-list.component.css',
 
})
export class StaffListComponent {
staffs: any[] = [];

  constructor(private staffService: StaffService) {}

  ngOnInit() {
    this.staffService.getStaffs().subscribe(data => this.staffs = data);
  }
}

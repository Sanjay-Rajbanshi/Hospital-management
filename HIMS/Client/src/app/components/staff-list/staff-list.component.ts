
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StaffService } from '../../services/staff.service';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AgGridModule } from 'ag-grid-angular';
import { ModuleRegistry, AllCommunityModule } from 'ag-grid-community';
import { HttpClientModule } from '@angular/common/http';
import { ChangeDetectorRef } from '@angular/core';

ModuleRegistry.registerModules([AllCommunityModule]);
declare var bootstrap: any;

@Component({
  selector: 'staff-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule, AgGridModule],
  templateUrl: './staff-list.component.html',
  styleUrls: ['./staff-list.component.css']
})
export class StaffListComponent implements OnInit {
  staffForm!: FormGroup;
  rowData: any[] = [];
  selectedStaffId: string | null = null;
  isEditMode = false;

  roles = [
    { value: 0, label: 'Doctor' },
    { value: 1, label: 'Nurse' },
    { value: 2, label: 'Admin' },
    { value: 3, label: 'Receptionist'}
  ];

  columnsDfs = [
    { headerName: 'Name', field: 'name', filter: 'agTextColumnFilter' },
    { headerName: 'Role', field: 'role', filter: 'agTextColumnFilter',
      valueFormatter: (params:any)=>{
        switch(params.value){
          case 0 : return 'Doctor';
          case 1: return 'Nurse';
          case 2: return 'Admin';
          case 3: return 'Receptionist';
          default: return '';
        }
      }
     },
    { headerName: 'Department', field: 'department' },
    // { headerName: 'Phone', field: 'phoneNumber' },
    // { headerName: 'DOB', field: 'dateOfBirth' },
    {
      headerName: 'Actions',
      field: 'actions',
      cellRenderer: () => `
        <button class="btn btn-info btn-sm view-btn">View</button>
        <button class="btn btn-primary btn-sm edit-btn">Edit</button>
        <button class="btn btn-danger btn-sm delete-btn">Delete</button>
      `,
      onCellClicked: (params: any) => {
        if (params.event.target.classList.contains('edit-btn')) this.editStaff(params.data);
        if (params.event.target.classList.contains('delete-btn')) this.deleteStaff(params.data.id);
        if (params.event.target.classList.contains('view-btn')) this.viewStaff(params.data);
      }
    }
  ];

  selectedStaff: any = null;
  isViewMode = false;
  modalInstance: any;

  constructor(
    private staffService: StaffService,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.staffForm = this.fb.group({
      name: ['', Validators.required],
      role: ['', Validators.required],
      department: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      dateOfBirth: ['', Validators.required]
    });
    this.loadStaff();
  }

  loadStaff() {
    this.staffService.getStaffs().subscribe(data => {
      this.rowData = [...data];
    });
  }

  addStaff() {
    if (this.staffForm.invalid) {
      this.staffForm.markAllAsTouched();
      return;
    }
    this.staffService.createStaff(this.staffForm.value).subscribe({
      next: () => {
        alert('Staff created successfully');
        this.loadStaff();
        this.resetForm();
      },
      error: err => console.error(err)
    });
  }

  editStaff(staff: any) {
    this.selectedStaffId = staff.id;
    this.isEditMode = true;
    this.staffForm.patchValue({
      name: staff.name,
      role: staff.role,
      department: staff.department,
      phoneNumber: staff.phoneNumber,
      dateOfBirth: staff.dateOfBirth?.substring(0,10)
    });
    this.openModal();
  }

  updateStaff() {
    if (!this.selectedStaffId) return;
    this.staffService.updateStaff(this.selectedStaffId, this.staffForm.value).subscribe({
      next: () => {
        alert('Staff updated successfully');
        this.loadStaff();
        this.resetForm();
      },
      error: err => console.error(err)
    });
  }

  deleteStaff(id: string) {
    if (confirm('Are you sure want to delete?')) {
      this.staffService.deleteStaff(id).subscribe({
        next: () => {
          alert('Staff deleted');
          this.loadStaff();
        },
        error: err => console.error(err)
      });
    }
  }

  viewStaff(staff: any) {
    this.selectedStaff = staff;
    this.isViewMode = true;
  }

  resetForm() {
    this.staffForm.reset();
    this.selectedStaffId = null;
    this.isEditMode = false;
  }

  openModal() {
    const modalEl = document.getElementById('staffModal');
    if (!this.modalInstance) {
      this.modalInstance = new bootstrap.Modal(modalEl);
    }
    this.modalInstance.show();
  }

  closeModal() {
    if (this.modalInstance) this.modalInstance.hide();
  }

  getRoleLabel(value:number):string{
    const role = this.roles.find(r=>r.value===value);
    return role? role.label: '';
  }
}
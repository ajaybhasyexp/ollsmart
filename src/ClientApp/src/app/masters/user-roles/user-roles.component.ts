import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserRole } from '../../models/UserRole';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { NgbModal,NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-user-roles',
  templateUrl: './user-roles.component.html',
  styleUrls: ['./user-roles.component.css']
})
export class UserRolesComponent implements OnInit {

  displayedColumns: string[] = ['UserRole','Status','Actions'];
  dataSource: MatTableDataSource<UserRole>;
  userRole = new UserRole();
  userRoles: Array<UserRole> = new Array<UserRole>();
  modalReference: NgbModalRef; 

  public btnSubmited = false;
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  userRoleForm = new FormGroup({
    userRoleName: new FormControl('', Validators.required), 
    status: new FormControl('', Validators.required)
  });
  baseUrl: string;
  constructor(  private http: HttpClient, 
     @Inject('BASE_URL') url: string,
    public dialog: MatDialog,
    private modalService: NgbModal) { 
       this.baseUrl=url;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  ngOnInit() {
    this.getUserRoles(); 
  }
  getUserRoles() {
    this.http.get<Array<UserRole>>(this.baseUrl + 'api/User/UserRoles').subscribe(result => {
      this.userRoles = result;
      this.dataSource = new MatTableDataSource(this.userRoles);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }, error => console.error(error));
  }
  OpenModal(content,id:number){
    this.userRole = new UserRole();   
    if(id>0)
    {
        this.http.get<UserRole>(this.baseUrl + 'api/User/UserRoleById/'+id).subscribe(result => {
        this.BindUserRole(result);
        this.userRole=result;
      }, error => console.error(error));
    }
    else{
      this.ClearForm();
    }
    this.modalReference=this.modalService.open(content);
  }
  BindUserRole(data:UserRole) {
    console.log(data);
    this.userRole=data;
    this.userRoleForm.setValue({
      userRoleName: data.userRoleName,
      status:data.isActive
    });
  }
  ClearForm() {
    this.btnSubmited = false;
    this.userRoleForm.reset();
  }
  SaveuserRole(){
    this.btnSubmited = true;
    if (this.userRoleForm.valid) {   
      this.btnSubmited = false;    
      this.userRole.userRoleName=this.userRoleForm.get('userRoleName').value; 
      this.userRole.isActive=this.userRoleForm.get('status').value; 

      this.userRole.createdBy=1;
        this.http.post(this.baseUrl + 'api/User/UserRole', this.userRole).subscribe(
          (response) => {
            this.modalReference.close();
            this.getUserRoles(); 
          },
          (error) => console.log(error)        
        )
    
    } 
    
  }
  

}

import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Category } from '../../models/category';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  displayedColumns: string[] = ['Category','Description','Parent Category','Status','Actions'];
  dataSource: MatTableDataSource<Category>;
  category = new Category();
  categories: Array<Category> = new Array<Category>();

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Array<Category>>(baseUrl + 'api/category').subscribe(result => {
      this.categories = result;
      this.dataSource = new MatTableDataSource(this.categories);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      console.log(this.categories);
    }, error => console.error(error));
    
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  ngOnInit() {
  }

}

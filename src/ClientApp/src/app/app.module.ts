import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { MastersComponent } from './masters/masters.component';
import { BrandsComponent } from './masters/brands/brands.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSliderModule } from '@angular/material/slider';
import {MatDialogModule} from '@angular/material/dialog';
import { MatTabsModule } from '@angular/material';
// import { FormWizardModule } from 'angular2-wizard';

// import {DialogContentExampleDialog} from './masters/brands/brands.component';
import {
  MatButtonModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatSelectModule,
  MatSidenavModule,
  MatCardModule,
  MatTableModule,
  MatPaginatorModule
} from "@angular/material";
import { CategoriesComponent } from './masters/categories/categories.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ParentCategoriesComponent } from './masters/parent-categories/parent-categories.component';
import { ProductsComponent } from './masters/products/products.component';
import { UnitsComponent } from './masters/units/units.component';
import { ProductPropertyComponent } from './masters/product-property/product-property.component';
import { ProductAttributesComponent } from './masters/products/product-attributes/product-attributes.component';
import { UserRolesComponent } from './masters/user-roles/user-roles.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    MastersComponent,
    BrandsComponent,
    CategoriesComponent,
    ParentCategoriesComponent,
    ProductsComponent,
    UnitsComponent,
    ProductPropertyComponent,
    ProductAttributesComponent,
    UserRolesComponent
    // DialogContentExampleDialog
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatSliderModule,
    FormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatListModule,
    MatInputModule,
    MatSelectModule,
    MatSidenavModule,
    MatCardModule,
    MatTableModule,
    MatPaginatorModule,
    MatDialogModule,
    ReactiveFormsModule,
    NgbModule,
    MatTabsModule,
    // FormWizardModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'masters', component: MastersComponent },
      { path: 'products', component: ProductsComponent },
      
    ]),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  // entryComponents: [DialogContentExampleDialog]
})
export class AppModule { }

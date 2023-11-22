// authors/authors.module.ts

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorsRoutingModule } from './authors-routing.module';
import { AuthorsListComponent } from './authors-list/authors-list.component';
import { AuthorDetailsComponent } from './author-details/author-details.component';
import { AuthorEditComponent } from './author-edit/author-edit.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AuthorAddComponent } from './author-add/author-add.component';

@NgModule({
  declarations: [
    AuthorsListComponent,
    AuthorDetailsComponent,
    AuthorEditComponent,
    AuthorAddComponent
  ],
  imports: [
    CommonModule,
    AuthorsRoutingModule,
    HttpClientModule,
    FormsModule
  ]
})
export class AuthorsModule { }

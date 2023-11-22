// authors/authors-routing.module.ts

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorsListComponent } from './authors-list/authors-list.component';
import { AuthorDetailsComponent } from './author-details/author-details.component';
import { AuthorEditComponent } from './author-edit/author-edit.component';
import { AuthorAddComponent } from './author-add/author-add.component';

const routes: Routes = [
  { path: 'authors', component: AuthorsListComponent },
  { path: 'authors/add', component: AuthorAddComponent },
  { path: 'authors/:id', component: AuthorDetailsComponent },
  { path: 'authors/:id/edit', component: AuthorEditComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorsRoutingModule { }

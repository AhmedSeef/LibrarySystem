// publishers/publishers-routing.module.ts

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PublishersListComponent } from './publishers-list/publishers-list.component';
import { PublisherDetailsComponent } from './publisher-details/publisher-details.component';
import { PublisherEditComponent } from './publisher-edit/publisher-edit.component';
import { PublisherAddComponent } from './publisher-add/publisher-add.component';

const routes: Routes = [
  { path: 'publishers', component: PublishersListComponent },
  { path: 'publishers/add', component: PublisherAddComponent },
  { path: 'publishers/:id', component: PublisherDetailsComponent },
  { path: 'publishers/:id/edit', component: PublisherEditComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublishersRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/pages/home/home.component';
import { CitiesComponent } from './components/pages/cities/cities.component';
import { PersonsComponent } from './components/pages/persons/persons.component';
import { AddOrEditComponent } from './components/pages/addoredit/addoredit.component';

const routes: Routes = [
  {
    path: 'cities', children: [
      { path: '', component: CitiesComponent },
      { path: 'add', component: AddOrEditComponent },
      { path: ':id', component: AddOrEditComponent },
      { path: ':id/edit', component: AddOrEditComponent }
    ]
  },
  { path: '', component: HomeComponent },
  {
    path: 'persons', children: [
      { path: '', component: PersonsComponent },
      { path: 'add', component: AddOrEditComponent },
      { path: ':id', component: AddOrEditComponent },
      { path: ':id/edit', component: AddOrEditComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }

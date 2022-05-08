import { Component, OnInit } from '@angular/core';
import { PersonDTO } from '../../../models/PersonDTO';
import { PersonsService } from '../../../providers/persons.service';

@Component({
  selector: 'app-persons',
  templateUrl: './persons.component.html',
  styleUrls: ['./persons.component.css']
})
export class PersonsComponent implements OnInit {
  persons: PersonDTO[] = [];

  constructor(private personsService: PersonsService) { }

  ngOnInit(): void {
    this.personsService.getAll().subscribe((x) => {
      this.persons = x;
    });
  }

}

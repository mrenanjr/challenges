import { Component, OnInit } from '@angular/core';
import { CityDTO } from '../../../models/CityDTO';
import { CitiesService } from '../../../providers/cities.service';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css']
})
export class CitiesComponent implements OnInit {
  cities: CityDTO[] = [];

  constructor(private citiesService: CitiesService) { }

  ngOnInit(): void {
    this.citiesService.getAll().subscribe((x) => {
      this.cities = x;
    });
  }

}

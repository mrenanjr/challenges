import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateOrUpdateCityDTO } from 'src/app/models/CreateOrUpdateCityDTO';
import { CreateOrUpdatePersonDTO } from 'src/app/models/CreateOrUpdatePersonDTO';
import { CitiesService } from 'src/app/providers/cities.service';
import { PersonsService } from 'src/app/providers/persons.service';
import { CityDTO } from '../../../models/CityDTO';

@Component({
  selector: 'app-addoredit',
  templateUrl: './addoredit.component.html',
  styleUrls: ['./addoredit.component.css']
})
export class AddOrEditComponent implements OnInit {
  id: string = '';
  city: CreateOrUpdateCityDTO = {
    name: '',
    uf: ''
  };
  person: CreateOrUpdatePersonDTO = {
    name: '',
    age: '',
    cityId: '',
    cpf: '',
    email: ''
  };
  cities: CityDTO[] = [];
  form: FormGroup;
  isEdit: boolean = false;
  isError: boolean = false;
  isCities: boolean = false;
  errors: string[] = [];

  constructor(
    private activatedRoute: ActivatedRoute,
    private citiesService: CitiesService,
    private personsService: PersonsService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.isCities = this.router.url.includes('cities');

    if (this.isCities) {
      this.form = this.formBuilder.group({
        name: new FormControl(this.city.name, []),
        uf: new FormControl(this.city.uf, [])
      })
    } else {
      this.form = this.formBuilder.group({
        name: new FormControl(this.person.name, []),
        age: new FormControl(this.person.age, []),
        cityId: new FormControl(this.person.cityId, []),
        cpf: new FormControl(this.person.cpf, []),
        email: new FormControl(this.person.email, []),
      })
    }
  }

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe((map) => {
      if (map.has('id')) {
        this.id = map.get('id') as string;
        if (!this.isCities) {
          this.getPersons(this.id);
        } else {
          this.getCity(this.id);
        }
      } else {
        if (!this.isCities)
          this.getCities();
      }
    });
  }

  getPersons(id: string) {
    this.personsService.getById(id).subscribe(x => {
      this.person = {
        name: x.name,
        age: x.age,
        cityId: x.city.id,
        cpf: x.cpf,
        email: x.contact.email
      }
      this.form.setValue(this.person);
      this.getCities();
      this.isEdit = true;
    });
  }

  getCity(id: string) {
    this.citiesService.getById(id).subscribe(x => {
      this.city = {
        name: x.name,
        uf: x.uf
      }
      this.form.setValue(this.city);
      this.isEdit = true;
    });
  }

  getCities() {
    this.citiesService.getAll().subscribe(x => {
      this.cities = x;
    });
  }

  getCityFromperson() {
    return this.cities.find(f => f.id == this.form.value.cityId)?.name;
  }

  changeCity(city: CityDTO) {
    this.form.setValue({ ...this.form.value, cityId: city.id });
    this.person.cityId = city.id; 
  }

  save() {
    let data: any = Object.fromEntries(Object.entries(this.form.value).filter(([_, value]: any) => value.length || typeof (value) == 'number'));
    if (this.isEdit) {
      if (this.isCities) {
        this.citiesService.update(this.id, <CreateOrUpdateCityDTO>data).subscribe({
          next: res => {
            this.back();
          },
          error: (err) => {
            this.handleError(err);
          }
        });
      } else {
        this.personsService.update(this.id, <CreateOrUpdatePersonDTO>data).subscribe({
          next: res => {
            this.back();
          },
          error: (err) => {
            this.handleError(err);
          }
        });
      }
    } else {
      if (this.isCities) {
        this.citiesService.create(<CreateOrUpdateCityDTO>data).subscribe({
          next: res => {
            this.back();
          },
          error: (err) => {
            this.handleError(err);
          }
        });
      } else {
        this.personsService.create(<CreateOrUpdatePersonDTO>data).subscribe({
          next: res => {
            this.back();
          },
          error: (err) => {
            this.handleError(err);
          }
        });
      }
    }
  }

  delete() {
    let response = confirm('Are you sure you want to delete?');
    if (response === true) {
      if (this.isCities) {
        this.citiesService.delete(this.id).subscribe({
          next: res => {
            this.back();
          },
          error: (err) => {
            this.handleError(err);
          }
        });
      } else {
        this.personsService.delete(this.id).subscribe({
          next: res => {
            this.back();
          },
          error: (err) => {
            this.handleError(err);
          }
        });
      }
    }
  }

  handleError(err: any) {
    if (err.ok === false) {
      this.isError = true;
      let e = err.error;
      if (e.error) this.errors = new Array(e.error);
      else if (e.errors) {
        this.errors = Object.entries(e.errors).map(([_, value]: any) => value.toString());
      }
    }
  }

  back() {
    if (this.isCities) this.router.navigate(['cities']);
    else this.router.navigate(['persons']);
  }
}

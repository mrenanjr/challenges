import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs";
import { CreateOrUpdateCityDTO } from "../models/CreateOrUpdateCityDTO";
import { CityDTO } from "../models/CityDTO";
import { Config } from "./config";

interface Response {
  success: boolean;
  error: boolean;
};

interface CitiesResponse extends Response {
  datas: CityDTO[];
}

interface CityResponse extends Response {
  data: CityDTO;
}

@Injectable({
  providedIn: 'root'
})
export class CitiesService {
  constructor(private http: HttpClient) { }

  get baseUri() {
    return `${Config.api}`;
  }

  getAll() {
    return this.http.get<CitiesResponse>(`${this.baseUri}/cities`).pipe(map(x => x.datas));
  }

  getById(id: string) {
    return this.http.get<CityResponse>(`${this.baseUri}/cities/${id}`).pipe(map(x => x.data));
  }

  add(item: CreateOrUpdateCityDTO) {
    return this.http.post<CityResponse>(`${this.baseUri}/cities`, item).pipe((map(x => x.data)));
  }

  create(item: CreateOrUpdateCityDTO) {
    return this.http.post<CityResponse>(`${this.baseUri}/cities`, item).pipe((map(x => x.data)));
  }

  update(id: string, item: CreateOrUpdateCityDTO) {
    return this.http.put<CityResponse>(`${this.baseUri}/cities/${id}`, item).pipe((map(x => x.data)));
  }

  delete(id: string) {
    return this.http.delete(`${this.baseUri}/cities/${id}`);
  }
}

import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs";
import { CreateOrUpdatePersonDTO } from "../models/CreateOrUpdatePersonDTO";
import { PersonDTO } from "../models/PersonDTO";
import { Config } from "./config";

interface Response {
  success: boolean;
  error: boolean;
};

interface PersonsResponse extends Response {
  datas: PersonDTO[];
}

interface PersonResponse extends Response {
  data: PersonDTO;
}

@Injectable({
  providedIn: 'root'
})
export class PersonsService {
  constructor(private http: HttpClient) { }

  get baseUri() {
    return `${Config.api}`;
  }

  getAll() {
    return this.http.get<PersonsResponse>(`${this.baseUri}/persons`).pipe(map(x => x.datas));
  }

  getById(id: string) {
    return this.http.get<PersonResponse>(`${this.baseUri}/persons/${id}`).pipe(map(x => x.data));
  }

  create(item: CreateOrUpdatePersonDTO) {
    return this.http.post<PersonResponse>(`${this.baseUri}/persons`, item).pipe((map(x => x.data)));
  }

  update(id: string, item: CreateOrUpdatePersonDTO) {
    return this.http.put<PersonResponse>(`${this.baseUri}/persons/${id}`, item).pipe((map(x => x.data)));
  }

  delete(id: string) {
    return this.http.delete(`${this.baseUri}/persons/${id}`);
  }
}

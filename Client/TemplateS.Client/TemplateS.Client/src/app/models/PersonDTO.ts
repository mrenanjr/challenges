import { CityDTO } from "./CityDTO";
import { ContactDTO } from "./ContactDTO";

export interface PersonDTO {
  id: string;
  name: string;
  age: string;
  cpf: string;
  city: CityDTO;
  contact: ContactDTO;
  createdDate: Date;
}

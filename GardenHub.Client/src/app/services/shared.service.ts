import {Injectable} from '@angular/core';
import {Location} from '@angular/common';
import StorageService from "./storage.service";


@Injectable({
  providedIn: 'root'
})
export class SharedService extends StorageService {
  API_URL = 'http://gardenhub1-001-site1.anytempurl.com/api';

  constructor(
    private location: Location
  ) {
    super();
  }

  setIdToURL(id: number) {
    this.location.replaceState(`${location.pathname}/${id}`);
  }

  convertName(name: string): string {
    const cyrillicPattern = /[а-яА-ЯЁё]/;
    if (cyrillicPattern.test(name)) {
      const en = ["j", "c", "u", "k", "e", "n", "g", "sh", "shch", "z", "h", "i", "e", "j", "d", "l", "o", "r", "p", "a", "w", "i", "f", "ja", "ch", "s", "m", "y", "t", "", "b", "y", "_"];
      const ua = ["й", "ц", "у", "к", "е", "н", "г", "ш", "щ", "з", "х", "ї", "є", "ж", "д", "л", "о", "р", "п", "а", "в", "і", "ф", "я", "ч", "с", "м", "и", "т", "ь", "б", "ю", " "];
      const cyr = name.toLowerCase().split('');
      const eng = [];
      for (let i = 0; i < cyr.length; i++) {
        for (let j = 0; j < ua.length; j++) {
          if (cyr[i] === ua[j]) {
            eng.push(en[j]);
          }
        }
      }
      return `${eng.join('')}_${Date.now()}`;
    } else {
      if (name) {
        return `${name.replaceAll(' ', '_')}_${Date.now()}`;
      } else {
        return `name_${Date.now()}`;
      }
    }
    return `name_${Date.now()}`;
  }

  generateParamsForRequest(params: {key: string, value: string}[], startSign: string): string {
    let result = '';
    params.map(item => {
      result += `${item.key}=${item.value}&`
    });
    result = `${startSign}${result.slice(0, -1)}`;
    return result;
  }
}

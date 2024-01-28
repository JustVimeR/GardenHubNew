import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export default abstract class StorageService {

  hasKeyInStorage(key: string): boolean {
    return Boolean(localStorage.getItem(key));
  }

  private hasItem(storage: any, id: number): boolean {
    const item = storage.find((el: any) => el.id === id);
    return Boolean(item);
  }

  getDataStorage(key: string): any {
    return localStorage.getItem(key) !== 'undefined' ? JSON.parse(localStorage.getItem(key)!) : [];
  }

  setDataStorage(key: string, value: object): void {
    if (value !== undefined && value !== null) {
      localStorage.setItem(key, JSON.stringify(value));
    }
  }

  removeDataStorage(key: string): void {
    localStorage.removeItem(key);
  }

  getStringStorage(key: string): string {
    return localStorage.getItem(key)!;
  }

  setStringStorage(key: string, value: string): void {
    if (value !== undefined && value !== null) {
      localStorage.setItem(key, value);
    }
  }

  getDataById(key: string, id: number): any {
    const data = JSON.parse(localStorage.getItem(key)!);
    const item = data.find((el: any) => el.id === id);
    return item;
  }

  updateStorageById(key: string, id: number, value: object): void {
    if (value !== undefined && value !== null) {
      let storage = this.getDataStorage(key);
      if (this.hasItem(storage, id)) {
        storage = storage.map((item: any) => {
          if (item.id === id) {
            item = value;
          }
          return item;
        });
      } else {
        storage.push(value);
      }
      this.setDataStorage(key, storage);
    }
  }
}

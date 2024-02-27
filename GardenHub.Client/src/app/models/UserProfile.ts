import { Media } from "./Media";
import { Gardener } from "./Gardener";
import { CustomerProfile } from "./CustomerProfile";

export interface UserProfile {
    icon: Media;
    name: string;
    surname: string;
    email: string;
    userName: string;
    phoneNumber: string;
    description: string;
    birthDate: string;
    gardenerProfile: Gardener;
    customerProfile: CustomerProfile;
  }
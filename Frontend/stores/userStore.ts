import { jwtDecode } from 'jwt-decode';
import { defineStore } from 'pinia';
//import { useStorage } from '@vueuse/core';
import type { AppUser } from '~/types/appUser';

export const useUserStore = defineStore('user', () => {
    const api = useApi();
    const user = ref<AppUser | null>(null);
    const token = ref<string | null>(null);
    const router = useRouter();

    const isAuthenticated = computed(() => !!token.value)

    const loadUser = async () => {
      const storedToken = localStorage.getItem('token');

      if (!storedToken) {
        user.value = null;
        return;
      }
      token.value = storedToken;

      const decodedToken = jwtDecode<{ id: number }>(storedToken);
      const userId = decodedToken.id;

      try {
          const userData = await api.customFetch<AppUser>(`ApplicationUser/${userId}`, {
            headers: {
                Authorization: `Bearer ${token.value}`
            }
          });
          user.value = userData;
      } catch(error) {
          console.error("Error loading user: ", error)
          user.value = null;
      }
    };

    const login = async (credentials: { email: string; password: string }) => {
      try {
        const response = await api.customFetch<{ user: AppUser; token: string }>('Account/login', {
          method: 'POST',
          body: credentials,
        });
        
        user.value = response.user;
        token.value = response.token;

        localStorage.setItem('token', response.token);
        await loadUser();
      } catch (error) {
        console.error('Login failed: ', error);
      }
    };

    const setUser = (newUser: AppUser) => {
      user.value = newUser;  
    };
    
    const setToken = (newToken: string) => {
      token.value = newToken;  
    };

    const register = async (registrationData: { username: string; email: string; password: string}) => {
        try {
            const response = await api.customFetch<{ user: AppUser; token: string}>('Account/register', {
                method: 'POST',
                body: registrationData,
            });
            user.value = response.user;
            token.value = response.token;

            localStorage.setItem('token', response.token)
            return response;
        } catch(error) {
            console.error("Registration failed: ", error)
        }
    };

    const logout = () => {
        user.value = null;
        token.value = null;

        localStorage.removeItem('token');
        router.push('/login');
    }

    //loadUser();

    return { user, token, isAuthenticated, setUser, setToken, loadUser, login, register, logout }
});
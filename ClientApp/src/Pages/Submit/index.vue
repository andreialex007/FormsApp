<script setup lang="ts">
import { Observer } from 'mobx-vue-lite'
import type { Props } from './Store'
import { countries, genderOptions, getFieldClasses } from './Store'
import ValidationError from './ValidationError.vue'

const props = defineProps<Props>()
</script>

<template>
  <Observer>
    <div class="min-h-[calc(100vh-4rem)]  py-12 px-4 sm:px-6 lg:px-8 bg-gray-50">
      <div class="max-w-2xl mx-auto">
        <!-- Success Message -->
        <div v-if="props.store.isSubmitted" class="text-center">
          <div class="bg-white rounded-2xl shadow-xl p-12">
            <div class="inline-flex items-center justify-center w-24 h-24 bg-emerald-100 rounded-full mb-6">
              <i class="ri-checkbox-circle-fill text-6xl text-emerald-600"></i>
            </div>
            <h2 class="text-3xl font-bold text-gray-900 mb-3">Success!</h2>
            <p class="text-gray-600 text-lg mb-8">Your information has been submitted successfully.</p>
            <button
              @click="props.store.isSubmitted = false; props.store.resetForm()"
              class="bg-emerald-600 text-white py-3 px-8 rounded-lg text-lg font-bold hover:bg-emerald-700 focus:ring-4 focus:ring-emerald-300 transition duration-200 shadow-lg hover:shadow-xl inline-flex items-center gap-2">
              <i class="ri-add-circle-line text-xl"></i>
              Submit Another
            </button>
          </div>
        </div>

        <!-- Form -->
        <div v-else>
          <!-- Header -->
          <div class="text-center mb-8">
            <div class="inline-flex items-center justify-center w-16 h-16 bg-gray-600 rounded-full mb-4">
              <i class="ri-file-add-fill text-3xl text-white"></i>
            </div>
            <h1 class="text-4xl font-bold text-gray-900 mb-2">Submit Your Information</h1>
            <p class="text-gray-600">Please fill out all the fields below</p>
          </div>

          <!-- Message Alert -->
          <div v-if="props.store.message"
             class="mb-6 p-4 rounded-xl flex items-start gap-3 shadow-lg transition-all duration-300 bg-red-50 border-2 border-red-200">
          <div class="flex-shrink-0 w-8 h-8 rounded-full flex items-center justify-center bg-red-100">
            <i class="ri-error-warning-fill text-xl text-red-600"></i>
          </div>
          <div class="flex-1 flex items-center font-bold text-red-700 h-full text-base/8">
            {{ props.store.message }}
          </div>
          <button
            @click="props.store.clearMessage()"
            class="flex-shrink-0 hover:bg-opacity-20 rounded-lg p-1 transition text-red-600 hover:bg-red-600">
            <i class="ri-close-line text-xl"></i>
          </button>
        </div>

        <!-- Form Card -->
        <div class="bg-white rounded-2xl shadow-xl p-8 ">
          <form @submit.prevent="props.store.triggerSubmit()" class="space-y-3">
            <!-- Full Name (Text) -->
            <div>
              <label for="fullName" class="flex items-center text-sm font-semibold text-gray-700 mb-2">
                <i class="ri-user-line mr-2 text-gray-600"></i>
                Full Name
              </label>
              <input
                id="fullName"
                v-model="props.store.fields.fullName.value"
                type="text"
                @blur="props.store.triggerValidate(props.store.fields.fullName)"
                :class="getFieldClasses(props.store.fields.fullName)"
                placeholder="Enter your full name"
              />
              <ValidationError :field="props.store.fields.fullName" />
            </div>

            <!-- Email (Text) -->
            <div>
              <label for="email" class="flex items-center text-sm font-semibold text-gray-700 mb-2">
                <i class="ri-mail-line mr-2 text-gray-600"></i>
                Email Address
              </label>
              <input
                id="email"
                v-model="props.store.fields.email.value"
                type="email"
                @blur="props.store.triggerValidate(props.store.fields.email)"
                :class="getFieldClasses(props.store.fields.email)"
                placeholder="your.email@example.com"
              />
              <ValidationError :field="props.store.fields.email" />
            </div>

            <!-- Country (Dropdown) -->
            <div>
              <label for="country" class="flex items-center text-sm font-semibold text-gray-700 mb-2">
                <i class="ri-global-line mr-2 text-gray-600"></i>
                Country
              </label>
              <select
                id="country"
                v-model="props.store.fields.country.value"
                @blur="props.store.triggerValidate(props.store.fields.country)"
                :class="getFieldClasses(props.store.fields.country, 'bg-white')"
              >
                <option value="">Select your country</option>
                <option v-for="c in countries" :key="c" :value="c">
                  {{ c }}
                </option>
              </select>
              <ValidationError :field="props.store.fields.country" />
            </div>

            <!-- Birth Date (Date) -->
            <div>
              <label for="birthDate" class="flex items-center text-sm font-semibold text-gray-700 mb-2">
                <i class="ri-calendar-line mr-2 text-gray-600"></i>
                Birth Date
              </label>
              <input
                id="birthDate"
                v-model="props.store.fields.birthDate.value"
                type="date"
                @blur="props.store.triggerValidate(props.store.fields.birthDate)"
                :class="getFieldClasses(props.store.fields.birthDate)"
              />
              <ValidationError :field="props.store.fields.birthDate" />
            </div>

            <!-- Gender (Radio) -->
            <div>
              <label class="flex items-center text-sm font-semibold text-gray-700 mb-3">
                <i class="ri-user-heart-line mr-2 text-gray-600"></i>
                Gender
              </label>
              <div class="flex gap-6">
                <label v-for="option in genderOptions" :key="option" class="flex items-center cursor-pointer group">
                  <input
                    v-model="props.store.fields.gender.value"
                    type="radio"
                    :value="option"
                    @blur="props.store.triggerValidate(props.store.fields.gender)"
                    class="w-5 h-5 text-gray-600 border-gray-300 focus:ring-gray-500 cursor-pointer"
                  />
                  <span class="ml-2 text-gray-700 group-hover:text-gray-600 transition">{{ option }}</span>
                </label>
              </div>
              <ValidationError :field="props.store.fields.gender" />
            </div>

            <!-- Newsletter (Checkbox) -->
            <div class="bg-gray-50 rounded-lg p-4 border border-gray-200">
              <label class="flex items-center cursor-pointer group">
                <input
                  v-model="props.store.fields.newsletter.value"
                  type="checkbox"
                  class="w-5 h-5 text-gray-600 border-gray-300 rounded focus:ring-gray-500 cursor-pointer"
                />
                <span class="ml-3 text-gray-700 group-hover:text-gray-600 transition">
                  <i class="ri-mail-send-line mr-1 text-gray-600"></i>
                  Subscribe to our newsletter
                </span>
              </label>
            </div>

            <!-- Submit Button -->
            <div class="pt-4">
              <button
                type="submit"
                :disabled="props.store.isSubmitting"
                class="w-full bg-emerald-600 text-white py-4 px-8 rounded-lg text-lg font-bold hover:bg-emerald-700 focus:ring-4 
                focus:ring-emerald-300 transition duration-200 disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center shadow-lg hover:shadow-xl"
              >
                <i v-if="!props.store.isSubmitting" class="ri-send-plane-fill mr-2 text-xl"></i>
                <i v-else class="ri-loader-4-line mr-2 animate-spin text-xl"></i>
                {{ props.store.isSubmitting ? 'Submitting...' : 'Submit Form' }}
              </button>
            </div>
          </form>
        </div>

          <!-- Info Footer -->
          <div class="text-center mt-6 text-sm text-gray-600">
            <i class="ri-shield-check-line mr-1"></i>
            Your information is secure and will never be shared
          </div>
        </div>
      </div>
    </div>
  </Observer>
</template>

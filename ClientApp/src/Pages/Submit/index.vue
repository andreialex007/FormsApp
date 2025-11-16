<script setup lang="ts">
import { Observer } from 'mobx-vue-lite'
import type { Props } from './Store'
import { countries } from './Store'
import ValidationError from './ValidationError.vue'

const props = defineProps<Props>()

const genderOptions = ['Male', 'Female', 'Other']
</script>

<template>
  <Observer>
    <div class="min-h-[calc(100vh-4rem)]  py-12 px-4 sm:px-6 lg:px-8 bg-gray-50">
      <div class="max-w-2xl mx-auto">
        <!-- Header -->
        <div class="text-center mb-8">
          <div class="inline-flex items-center justify-center w-16 h-16 bg-gray-600 rounded-full mb-4">
            <i class="ri-file-add-fill text-3xl text-white"></i>
          </div>
          <h1 class="text-4xl font-bold text-gray-900 mb-2">Submit Your Information</h1>
          <p class="text-gray-600">Please fill out all the fields below</p>
        </div>

        <!-- Form Card -->
        <div class="bg-white rounded-2xl shadow-xl p-8 ">
          <form @submit.prevent="props.store.submitForm()" class="space-y-3">
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
                :class="[
                  'w-full px-4 py-3 rounded-lg transition duration-200 outline-none',
                  props.store.fields.fullName.dirty && props.store.fields.fullName.error
                    ? 'border-2 border-red-500 focus:ring-2 focus:ring-red-500'
                    : 'border border-gray-300 focus:ring-2 focus:ring-gray-500 focus:border-transparent'
                ]"
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
                :class="[
                  'w-full px-4 py-3 rounded-lg transition duration-200 outline-none',
                  props.store.fields.email.dirty && props.store.fields.email.error
                    ? 'border-2 border-red-500 focus:ring-2 focus:ring-red-500'
                    : 'border border-gray-300 focus:ring-2 focus:ring-gray-500 focus:border-transparent'
                ]"
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
                :class="[
                  'w-full px-4 py-3 rounded-lg transition duration-200 outline-none bg-white',
                  props.store.fields.country.dirty && props.store.fields.country.error
                    ? 'border-2 border-red-500 focus:ring-2 focus:ring-red-500'
                    : 'border border-gray-300 focus:ring-2 focus:ring-gray-500 focus:border-transparent'
                ]"
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
                :class="[
                  'w-full px-4 py-3 rounded-lg transition duration-200 outline-none',
                  props.store.fields.birthDate.dirty && props.store.fields.birthDate.error
                    ? 'border-2 border-red-500 focus:ring-2 focus:ring-red-500'
                    : 'border border-gray-300 focus:ring-2 focus:ring-gray-500 focus:border-transparent'
                ]"
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
                    @blur="props.store.fields.gender.dirty = true"
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
                class="w-full bg-emerald-600 text-white py-4 px-8 rounded-lg text-lg font-bold hover:bg-emerald-700 focus:ring-4 focus:ring-emerald-300 transition duration-200 disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center shadow-lg hover:shadow-xl"
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
  </Observer>
</template>
